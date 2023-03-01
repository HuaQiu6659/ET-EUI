using System;
using System.Data;
using System.Security.Principal;


namespace ET
{
    [FriendClass(typeof(ServerInfosComponent)),
        FriendClass(typeof(RoleInfosComponent))]
    [FriendClass(typeof(ET.RoleInfo))]
    public static class LoginHelper
    {
        public static async ETTask<A2C_Verification> SendRegistVerification(Scene zoneScene, string email)
        {
            A2C_Verification response = null;
            Session session = null;

            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(ConstValue.LoginAddress));
                response = (A2C_Verification)await session.Call(new C2A_Verification() { EMail = email });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            session?.Dispose();

            return response;
        }

        public static async ETTask<int> Regist(Scene zoneScene, string account, string password, string email)
        {
            A2C_Regist response = null;
            Session session = null;

            try
            {
                session = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(ConstValue.LoginAddress));
                response = (A2C_Regist)await session.Call(new C2A_Regist() { Account = account, Password = MD5Helper.StringMD5(password), EMail = MD5Helper.StringMD5(email) });

                return Finish(response.Error);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return Finish(ErrorCode.ERR_NetworkError);
            }

            //登录失败，断开连接
            int Finish(int errorCode)
            {
                session?.Dispose();
                return errorCode;
            }
        }

        public static async ETTask<int> Login(Scene zoneScene, string account, string password)
        {
            A2C_LoginAccount a2C_Login = null;
            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(ConstValue.LoginAddress));
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

        /// <summary>
        /// 获取角色列表并保存到管理组件中
        /// </summary>
        /// <param name="zoneScene"></param>
        /// <returns></returns>
        public static async ETTask<int> GetRoles(Scene zoneScene)
        {
            //请求角色列表
            var session = zoneScene.GetComponent<SessionComponent>().Session;
            var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();
            var serverInfos = zoneScene.GetComponent<ServerInfosComponent>();

            A2C_GetRolesResponse response = null;
            try
            {
                response = (A2C_GetRolesResponse)await session.Call(new C2A_GetRolesRequest()
                {
                    AccountId = accountInfo.AccountId,
                    ServerId = serverInfos.currentServerId,
                    Token = accountInfo.Token
                });
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

            //保存角色列表信息
            var rolesCmp = zoneScene.GetComponent<RoleInfosComponent>();
            rolesCmp.ClearRoles();
            if (response.Roles?.Count > 0)
            {
                foreach (var proto in response.Roles)
                {
                    var newRole = new RoleInfo();
                    newRole.FromMessage(proto);

                    //Freeze状态为已被删除的角色
                    if (newRole.status == RoleStatus.Freeze)
                    {
                        newRole.Dispose();
                        continue;
                    }
                    rolesCmp.roles.Add(newRole);
                }
            }

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
                response = (A2C_DeleteRoleResponse)await session.Call(new C2A_DeleteRoleRequest()
                {
                    AccountId = accountInfo.AccountId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().currentServerId,
                    RoleName = roleName,
                    Token = accountInfo.Token
                });
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

            //移除组件中的信息记录
            zoneScene.GetComponent<RoleInfosComponent>().RemoveRole(roleName);
            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> GetRealmKey(Scene zoneScene)
        {
            var session = zoneScene.GetComponent<SessionComponent>().Session;
            var accountInfo = zoneScene.GetComponent<AccountInfoComponent>();

            A2C_GetRealmKeyResponse response = null;
            try
            {
                response = (A2C_GetRealmKeyResponse) await session.Call(new C2A_GetRealmKeyRequest()
                {
                    AccountId = accountInfo.AccountId,
                    ServerId = zoneScene.GetComponent<ServerInfosComponent>().currentServerId,
                    Token = accountInfo.Token
                });
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

            //保存令牌、网关地址
            accountInfo.RealmAddress = response.RealmAddress;
            accountInfo.RealmKey = response.RealmKey;
            session.Dispose();  //获取到新网关地址，与账号服务器断开

            return ErrorCode.ERR_Success;
        }

        public static async ETTask<int> EnterGame(Scene zoneScene)
        {
            var accountInfoCmp = zoneScene.GetComponent<AccountInfoComponent>();
            string realmAddress = accountInfoCmp.RealmAddress;
            var netKcpCmp = zoneScene.GetComponent<NetKcpComponent>();

            //1.连接Realm，获取分配的Gate
            R2C_LoginRealmResponse r2C_LoginRealmResponse = null;
            Session session = netKcpCmp.Create(NetworkHelper.ToIPEndPoint(realmAddress));
            try
            {
                r2C_LoginRealmResponse = (R2C_LoginRealmResponse)await session.Call(new C2R_LoginRealmRequest() 
                { 
                    AccountId = accountInfoCmp.AccountId, 
                    RealmKey = accountInfoCmp.RealmKey 
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return ErrorCode.ERR_NetworkError;
            }
            finally
            {
                session?.Dispose();
                session = null;
            }

            if (r2C_LoginRealmResponse.Error != ErrorCode.ERR_Success)
                return r2C_LoginRealmResponse.Error;

            Log.Warning($"GateAddress:{r2C_LoginRealmResponse.GateAddress}");
            session = netKcpCmp.Create(NetworkHelper.ToIPEndPoint(r2C_LoginRealmResponse.GateAddress));
            session.AddComponent<PingComponent>();
            zoneScene.GetComponent<SessionComponent>().Session = session;

            //2.连接Gate
            long currentRoleId = zoneScene.GetComponent<RoleInfosComponent>().currentRoleId;
            G2C_LoginGameGateResponse g2C_LoginGate = null;
            try
            {
                g2C_LoginGate = (G2C_LoginGameGateResponse)await session.Call(new C2G_LoginGameGateRequest()
                {
                    AccountId = accountInfoCmp.AccountId,
                    Key = r2C_LoginRealmResponse.GateSessionKey,
                    RoleId = currentRoleId
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return ErrorCode.ERR_NetworkError;
            }
            if (g2C_LoginGate.Error != ErrorCode.ERR_Success)
            {
                session.Dispose();
                return g2C_LoginGate.Error;
            }
            Log.Debug("登录Gate成功");

            //3.角色正式请求进入游戏逻辑服
            G2C_EnterGameResponse enterGameResponse = null;
            try
            {
                enterGameResponse = (G2C_EnterGameResponse)await session.Call(new C2G_EnterGameRequest());
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                session.Dispose();
                return ErrorCode.ERR_NetworkError;
            }

            if (enterGameResponse.Error != ErrorCode.ERR_Success)
            {
                Log.Error(enterGameResponse.Error.ToString());
                return enterGameResponse.Error;
            }
            Log.Debug("进入游戏成功");

            return ErrorCode.ERR_Success;
        }
    }
}