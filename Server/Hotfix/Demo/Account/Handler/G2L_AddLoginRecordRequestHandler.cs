using System;

namespace ET
{
    public class G2L_AddLoginRecordRequestHandler : AMActorRpcHandler<Scene, G2L_AddLoginRecordRequest, L2G_AddLoginRecordResponse>
    {
        protected override async ETTask Run(Scene scene, G2L_AddLoginRecordRequest request, L2G_AddLoginRecordResponse response, Action reply)
        {
            long accountId = request.AccountId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLocker, accountId))
            {
                scene.GetComponent<LoginInfoRecordComponent>().AddOrSet(accountId, request.ServerId);
            }
            reply();
        }
    }
}
