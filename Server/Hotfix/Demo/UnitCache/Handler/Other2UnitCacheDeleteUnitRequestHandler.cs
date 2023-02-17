using System;

namespace ET
{
    public class Other2UnitCache_DeleteUnitRequestHandler : AMActorRpcHandler<Scene, Other2UnitCache_DeleteUnitRequest, UnitCache2Other_DeleteUnitResponse>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_DeleteUnitRequest request, UnitCache2Other_DeleteUnitResponse response, Action reply)
        {
            scene.GetComponent<UnitCacheComponent>().Delete(request.UnitId);
            reply();
            await ETTask.CompletedTask;
        }
    }
}
