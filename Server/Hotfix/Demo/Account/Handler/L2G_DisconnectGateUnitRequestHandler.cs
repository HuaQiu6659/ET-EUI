using System;

namespace ET
{
    public class L2G_DisconnectGateUnitRequestHandler : AMActorRpcHandler<Scene, L2G_DisconnectGateUnitRequest, G2L_DisconnectGateUnitResponse>
    {
        protected override async ETTask Run(Scene scene, L2G_DisconnectGateUnitRequest request, G2L_DisconnectGateUnitResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, accountId.GetHashCode()))
            {
                //检测该账号是否在Gate中
                var playerCmp = scene.GetComponent<PlayerComponent>();
                Player gateUnit = playerCmp.Get(accountId);
                if (gateUnit is null)   //不在
                {
                    reply();
                    return;
                }

                playerCmp.Remove(accountId);
                gateUnit.Dispose(); //暂时是直接断线
            }
            reply();

            await ETTask.CompletedTask;
        }
    }
}
