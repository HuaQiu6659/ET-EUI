using System;

namespace ET
{
    public class A2R_GetRealmKeyRequestHandler : AMActorRpcHandler<Scene, A2R_GetRealmKeyRequest, R2A_GetRealmKeyResponse>
    {
        protected override async ETTask Run(Scene scene, A2R_GetRealmKeyRequest request, R2A_GetRealmKeyResponse response, Action reply)
        {
            if (scene.SceneType != SceneType.Realm)
            {
                Log.Error($"请求Scene错误，目标Scene：Realm，当前Scene：{scene.SceneType}");
                response.Error = ErrorCode.ERR_WrongScene;
                reply();
                return;
            }

            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt64().ToString();
            var tokensCmp = scene.GetComponent<TokensComponent>();
            tokensCmp.AddOrSet(request.AccountId, key);
            response.RealmKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
