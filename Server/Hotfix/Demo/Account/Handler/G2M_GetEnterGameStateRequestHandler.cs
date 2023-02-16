using System;

namespace ET
{
    public class G2M_GetEnterGameStateRequestHandler : AMActorLocationRpcHandler<Unit, G2M_GetEnterGameStateRequest, M2G_GetEnterGameStateResponse>
    {
        protected override async ETTask Run(Unit unit, G2M_GetEnterGameStateRequest request, M2G_GetEnterGameStateResponse response, Action reply)
        {
            reply();
            await ETTask.CompletedTask;
        }
    }
}
