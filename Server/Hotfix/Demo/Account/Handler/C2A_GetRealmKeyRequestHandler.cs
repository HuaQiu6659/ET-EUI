using System;

namespace ET
{
    public class C2A_GetRealmKeyHandler : AMRpcHandler<C2A_GetRealmKey, A2C_GetRealmKey>
    {
        protected override async ETTask Run(Session session, C2A_GetRealmKey request, A2C_GetRealmKey response, Action reply)
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

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginOrRegiste, request.AccountId))
                {
                    StartSceneConfig realmStartSceneConfig = RealmGateAddressHelper.GetRealm(request.ServerId);
                    R2A_GetRealmKeyResponse r2A_GetRealmKeyResponse = (R2A_GetRealmKeyResponse) await MessageHelper.CallActor(realmStartSceneConfig.InstanceId, new A2R_GetRealmKeyRequest(){ AccountId = request.AccountId });

                    if (r2A_GetRealmKeyResponse.Error != ErrorCode.ERR_Success)
                    {
                        RequestError(r2A_GetRealmKeyResponse.Error);
                        return;
                    }

                    response.RealmKey = r2A_GetRealmKeyResponse.RealmKey;
                    response.RealmAddress = realmStartSceneConfig.OuterIPPort.ToString();
                    RequestError(ErrorCode.ERR_Success);
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
