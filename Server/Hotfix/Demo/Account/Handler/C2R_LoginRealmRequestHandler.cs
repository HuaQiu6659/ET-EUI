using System;

namespace ET
{
    public class C2R_LoginRealmRequestHandler : AMRpcHandler<C2R_LoginRealmRequest, R2C_LoginRealmResponse>
    {
        protected override async ETTask Run(Session session, C2R_LoginRealmRequest request, R2C_LoginRealmResponse response, Action reply)
        {
            if (!session.CheckScene(SceneType.Realm))
                return;

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            //判定Token
            var scene = session.DomainScene();
            var tokenCmp = scene.GetComponent<TokensComponent>();
            //Token不存在  或者  对不上
            if (!tokenCmp.Get(request.AccountId, out string key) || !key.Equals(request.RealmKey))
            {
                RequestError(ErrorCode.ERR_WrongToken);
                return;
            }

            //在此之后不会再用到该令牌，不会再连接Realm
            tokenCmp.Remove(request.AccountId);

            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginRealm, request.AccountId))
            {
                //固定分配一个Gate
                StartSceneConfig config = RealmGateAddressHelper.GetGate(scene.Zone, request.AccountId);

                G2R_GetLoginGateKeyResponse g2R_GetLoginKey = (G2R_GetLoginGateKeyResponse)await MessageHelper.CallActor(config.InstanceId, new R2G_GetLoginGateKeyRequest() { AccountId = request.AccountId });
                if (g2R_GetLoginKey.Error != ErrorCode.ERR_Success)
                {
                    RequestError(g2R_GetLoginKey.Error);
                    return;
                }

                response.GateSessionKey = g2R_GetLoginKey.GateSessionKey;
                response.GateAddress = config.OuterIPPort.ToString();
                RequestError(ErrorCode.ERR_Success);
            }

            void RequestError(int errorCode, bool disconnect = false)
            {
                response.Error = errorCode;
                reply();

                if (disconnect)
                    session?.Disconnect().Coroutine();
            }
        }
    }
}
