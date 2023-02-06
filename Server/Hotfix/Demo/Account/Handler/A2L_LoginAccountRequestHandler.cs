using System;

namespace ET
{
    public class A2L_LoginAccountRequestHandler : AMActorRpcHandler<Scene, A2L_LoginAccountRequest, L2A_LoginAccountResponse>
    {
        protected override async ETTask Run(Scene scene, A2L_LoginAccountRequest request, L2A_LoginAccountResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLocker, accountId.GetHashCode()))
            {
                //无登录信息，直接返回
                var loginInfoRecordCmp = scene.GetComponent<LoginInfoRecordComponent>();
                if (!loginInfoRecordCmp.Get(accountId, out int zone))
                {
                    reply();
                    return;
                }

                //通过网关均衡获取网关地址
                StartSceneConfig gateConfig = RealmGateAddressHelper.GetGate(zone, accountId);

                var g2L_DisconnectGateUnitResponse = (G2L_DisconnectGateUnitResponse) await MessageHelper.CallActor(gateConfig.InstanceId, new L2G_DisconnectGateUnitRequest() { AccountId = accountId });
                response.Error = g2L_DisconnectGateUnitResponse.Error;
                reply();
            }
        }
    }
}
