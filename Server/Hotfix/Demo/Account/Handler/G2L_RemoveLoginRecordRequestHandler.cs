using System;

namespace ET
{
    public class G2L_RemoveLoginRecordRequestHandler : AMActorRpcHandler<Scene, G2L_RemoveLoginRecordRequest, L2G_RemoveLoginRecordResponse>
    {
        protected override async ETTask Run(Scene scene, G2L_RemoveLoginRecordRequest request, L2G_RemoveLoginRecordResponse response, Action reply)
        {
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginCenterLocker, request.AccountId))
            {
                var loginInfoRecordCmp = scene.GetComponent<LoginInfoRecordComponent>();
                if (loginInfoRecordCmp.Get(request.AccountId, out int zone) && request.ServerId == zone)
                    loginInfoRecordCmp.Remove(request.AccountId);
            }
            reply();
        }
    }
}
