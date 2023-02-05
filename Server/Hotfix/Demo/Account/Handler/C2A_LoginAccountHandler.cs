using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    //登录请求处理
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            var currentScene = session.DomainScene().SceneType;
            if (currentScene != SceneType.Account)
            {
                Log.Error($"请求Scene错误，目标Scene：Account，当前Scene：{currentScene}");
                session.Dispose();
            }

            //带有该组件时新创建的连接只会维持5秒，当前视为通过连接验证，因此去除该限制
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //判定账号密码是否正确
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password))
            {
                LoginError(ErrorCode.ERR_EmptyAccount);
            }

            await ETTask.CompletedTask;

            void LoginError(int errorCode)
            {
                response.Error = errorCode;
                reply();
                
                session.Dispose();
            }
        }
    }
}
