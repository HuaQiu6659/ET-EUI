using System;


namespace ET
{
    [FriendClass(typeof(ServerInfosComponent)),
        FriendClass(typeof(RoleInfosComponent))]
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2C_Login = null;
            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                a2C_Login = (A2C_LoginAccount)await accountSession.Call(new C2A_LoginAccount() { Account = account, Password = MD5Helper.StringMD5(password) });

                if (a2C_Login.Error != ErrorCode.ERR_Success)
                    return LoginError(a2C_Login.Error);

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return LoginError(ErrorCode.ERR_NetworkError);
            }

            //登录成功 保存连接 保存Token
            zoneScene.AddComponent<SessionComponent>().Session = accountSession;
            accountSession.AddComponent<PingComponent>();   //心跳检测，回复服务器心跳消息

            var accountInfoCmp = zoneScene.GetComponent<AccountInfoComponent>();
            accountInfoCmp.Token = a2C_Login.Token;
            accountInfoCmp.AccountId = a2C_Login.AccountId;

            return ErrorCode.ERR_Success;

            //登录失败，断开连接
            int LoginError(int errorCode)
            {
                accountSession?.Dispose();
                return errorCode;
            }
        }

        public static async ETTask<int> GetServerInfos(Scene zoneScene)
        {
            A2C_GetServerInfoResponse response = null;
            try
            {
                AccountInfoComponent accountInfo = zoneScene.GetComponent<AccountInfoComponent>();
                response = (A2C_GetServerInfoResponse)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_GetServerInfoRequest()
                {
                    AccountId = accountInfo.AccountId,
                    Token = accountInfo.Token
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            if (response.Error != ErrorCode.ERR_Success)
                return response.Error;

            //成功时保存区服信息
            zoneScene.GetComponent<ServerInfosComponent>().Add(response.ServerInfos);

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> CreateRole(Scene zoneScene, string roleName)
        {
            A2C_CreateRoleResponse response = null;

            try
            {
                AccountInfoComponent accountInfoComponent = zoneScene.GetComponent<AccountInfoComponent>();
                response = (A2C_CreateRoleResponse)await zoneScene.GetComponent<SessionComponent>().Session.Call(new C2A_CreateRoleRequest()
                {
                    RoleName = roleName,
                    AccountId = accountInfoComponent.AccountId,
                    Token = accountInfoComponent.Token,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().currentServerId
                });

            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return ErrorCode.ERR_NetworkError;
            }

            //创建失败
            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error(response.Error.ToString());
                return response.Error;
            }

            //存入列表
            var roleInfosCmp = zoneScene.GetComponent<RoleInfosComponent>();
            RoleInfo newRole = roleInfosCmp.AddChild<RoleInfo>();
            newRole.FromMessage(response.RoleInfo);
            roleInfosCmp.roles.Add(newRole);

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> DeleteRole(Scene zoneScene, string roleName)
        {
            var session = zoneScene.GetComponent<SessionComponent>().Session;
            var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();

            A2C_DeleteRoleResponse response = null;

            try
            {
                response = (A2C_DeleteRoleResponse)await session.Call(new C2A_CreateRoleRequest()
                {
                    AccountId = accountInfo.AccountId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().currentServerId,
                    RoleName = roleName,
                    Token = accountInfo.Token
                }) ;
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return ErrorCode.ERR_NetworkError;
            }

            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error(response.Error.ToString());
                return response.Error;
            }

            return ErrorCode.ERR_Success;
        }
    }
}