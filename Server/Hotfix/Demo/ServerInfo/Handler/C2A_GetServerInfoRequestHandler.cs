using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(ServerInfoManagerComponent))]
    public class C2A_GetServerInfoHandler : AMRpcHandler<C2A_GetServerInfo, A2C_GetServerInfo>
    {
        protected override async ETTask Run(Session session, C2A_GetServerInfo request, A2C_GetServerInfo response, Action reply)
        {
            if (!session.CheckScene(SceneType.Account))
                return;

            var sessionDomainScene = session.DomainScene();
            //Token不存在  或者  对不上
            if (!sessionDomainScene.GetComponent<TokensComponent>().Get(request.AccountId, out string token) || !token.Equals(request.Token))
            {
                RequestError(ErrorCode.ERR_WrongToken);
                return;
            }

            //从信息管理组件中读取区服信息
            foreach (var info in sessionDomainScene.GetComponent<ServerInfoManagerComponent>().serverInfos)
                response.ServerInfos.Add(info.ToMessage());
            reply();

            await ETTask.CompletedTask;

            void RequestError(int errorCode)
            {
                response.Error = errorCode;
                reply();
                session?.Disconnect().Coroutine();
            }
        }
    }
}
