using System;


namespace ET
{
    public static class LoginHelper
    {
        public static async ETTask<int> Login(Scene zoneScene, string address, string account, string password)
        {
            A2C_LoginAccount a2C_Login = null;
            Session accountSession = null;

            try
            {
                accountSession = zoneScene.GetComponent<NetKcpComponent>().Create(NetworkHelper.ToIPEndPoint(address));
                a2C_Login = (A2C_LoginAccount)await accountSession.Call(new C2A_LoginAccount() { Account = account, Password = password });

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

            var accountInfoCmp = zoneScene.GetComponent<AccountInfoComponent>();
            accountInfoCmp.token = a2C_Login.Token;
            accountInfoCmp.accountId = a2C_Login.AccountId;

            return ErrorCode.ERR_Success;

            //登录失败，断开连接
            int LoginError(int errorCode)
            {
                accountSession?.Dispose();
                return errorCode;
            }
        } 
    }
}