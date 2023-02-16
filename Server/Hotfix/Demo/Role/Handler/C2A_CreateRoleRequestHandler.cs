using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ET.Demo.Role.Handler
{
    [FriendClass(typeof(RoleInfo))]
    public class C2A_CreateRoleRequestHandler : AMRpcHandler<C2A_CreateRoleRequest, A2C_CreateRoleResponse>
    {
        protected override async ETTask Run(Session session, C2A_CreateRoleRequest request, A2C_CreateRoleResponse response, Action reply)
        {
            var currentScene = session.DomainScene().SceneType;
            if (currentScene != SceneType.Account)
            {
                Log.Error($"请求Scene错误，目标Scene：Account，当前Scene：{currentScene}");
                session.Dispose();
                return;
            }

            //避免短时间多次相同请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            //判定Token
            var sessionDomainScene = session.DomainScene();
            //Token不存在  或者  对不上
            if (!sessionDomainScene.GetComponent<TokensComponent>().Get(request.AccountId, out string token) || !token.Equals(request.Token))
            {
                RequestError(ErrorCode.ERR_WrongToken);
                return;
            }

            //判断角色名称输入
            if (string.IsNullOrEmpty(request.RoleName))
            {
                RequestError(ErrorCode.ERR_EmptyInput);
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                //避免同名同时创建
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.CreateRole, request.AccountId))
                {
                    //查询角色名是否已经存在
                    var db = DBManagerComponent.Instance.GetZoneDB(session.DomainZone());
                    var rolesInDb = await db.Query<RoleInfo>(d => d.name.Equals(request.RoleName) && d.serverId == request.ServerId);
                    if (rolesInDb?.Count > 0)
                    {
                        RequestError(ErrorCode.ERR_RoleExisted);
                        return;
                    }

                    //角色名不存在，新建角色
                    RoleInfo newRole = session.AddChildWithId<RoleInfo>(IdGenerater.Instance.GenerateUnitId(request.ServerId));
                    newRole.name = request.RoleName;
                    newRole.accountId = request.AccountId;
                    newRole.serverId = request.ServerId;
                    newRole.lastLoginTime = 0;
                    newRole.createTime = TimeHelper.ServerNow();
                    newRole.status = RoleStatus.Normal;

                    await db.Save(newRole);

                    response.RoleInfo = newRole.ToMessage();
                    reply();
                    newRole.Dispose();
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
