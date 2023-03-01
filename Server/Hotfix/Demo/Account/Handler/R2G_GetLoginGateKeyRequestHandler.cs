using System;

namespace ET
{
    public class R2G_GetLoginGateKeyRequestHandler : AMActorRpcHandler<Scene, R2G_GetLoginGateKeyRequest, G2R_GetLoginGateKeyResponse>
    {
        protected override async ETTask Run(Scene scene, R2G_GetLoginGateKeyRequest request, G2R_GetLoginGateKeyResponse response, Action reply)
        {
            if (!scene.CheckScene(SceneType.Gate, response, reply))
                return;

            //生成连接令牌
            string key = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt32().ToString();
            
            var gateSessionKeyCmp = scene.GetComponent<GateSessionKeyComponent>();
            gateSessionKeyCmp.Remove(request.AccountId);
            gateSessionKeyCmp.Add(request.AccountId, key);

            response.GateSessionKey = key;
            reply();
            await ETTask.CompletedTask;
        }
    }
}
