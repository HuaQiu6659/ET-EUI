using System;

namespace ET.Demo.Role.Handler
{
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public class C2A_DeleteRoleRequestHandler : AMRpcHandler<C2A_DeleteRoleRequest, A2C_DeleteRoleResponse>
    {
        protected override async ETTask Run(Session session, C2A_DeleteRoleRequest request, A2C_DeleteRoleResponse response, Action reply)
        {
            if (!session.CheckScene(SceneType.Account))
                return;

            //判定Token
            var sessionDomainScene = session.DomainScene();
            //Token不存在  或者  对不上
            if (!sessionDomainScene.GetComponent<TokensComponent>().Get(request.AccountId, out string token) || !token.Equals(request.Token))
            {
                RequestError(ErrorCode.ERR_WrongToken);
                return;
            }

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            //避免同一账号多次请求
            using (session.AddComponent<SessionLockingComponent>())
            {
                //避免不同账号相同请求
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.DeleteRole, request.AccountId))
                {
                    var db = DBManagerComponent.Instance.GetZoneDB(session.DomainZone());

                    var target = await db.Query<RoleInfo>(info =>
                        info.name.Equals(request.RoleName)
                        && info.accountId == request.AccountId
                        && info.serverId == request.ServerId);

                    if (target?.Count < 1)
                    {
                        response.Error = ErrorCode.ERR_RoleUnexisted;
                        reply();
                        return;
                    }

                    long result = await db.Remove<RoleInfo>(info =>
                        info.name.Equals(request.RoleName)
                        && info.accountId == request.AccountId
                        && info.serverId == request.ServerId);

                    //await db.Remove<RoleInfo>(target[0].Id);
                    //不应该直接删除数据库，而是修改角色状态为冻结
                    var role = target[0];
                    session.AddChild(role);
                    role.status = RoleStatus.Freeze;
                    await db.Save(role);
                    role.Dispose();

                    response.Error = ErrorCode.ERR_Success;
                    reply();
                    target.ClearEntityList();
                }
            }

            void RequestError(int errorCode)
            {
                response.Error = errorCode;
                reply();
                session?.Disconnect().Coroutine();
            }
        }
    }
}
