using System;

namespace ET.Demo.Role.Handler
{
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public class C2A_GetRolesRequestHandler : AMRpcHandler<C2A_GetRolesRequest, A2C_GetRolesResponse>
    {
        protected override async ETTask Run(Session session, C2A_GetRolesRequest request, A2C_GetRolesResponse response, Action reply)
        {
            var currentScene = session.DomainScene().SceneType;
            if (currentScene != SceneType.Account)
            {
                Log.Error($"请求Scene错误，目标Scene：Account，当前Scene：{currentScene}");
                session.Dispose();
                return;
            }

            //避免短时间多次相同请求
            if (session.GetComponent<SessionLoginComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            using (session.AddComponent<SessionLoginComponent>())
            {
                var db = DBManagerComponent.Instance;
                var roleList = await db.GetZoneDB(session.DomainZone()).Query<RoleInfo>(info => info.accountId == request.AccountId && info.serverId == request.ServerId && info.status == RoleStatus.Normal);
                if (roleList?.Count > 0)
                {
                    foreach (var role in roleList)
                    {
                        response.Roles.Add(role.ToMessage());
                        role.Dispose();
                    }
                    roleList.Clear();
                }

                reply();
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
