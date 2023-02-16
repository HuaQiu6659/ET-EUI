using System;

namespace ET
{
    public class G2M_ExitGameRequestHandler : AMActorLocationRpcHandler<Unit, G2M_ExitGameRequest, M2G_ExitGameResponse>
    {
        protected override async ETTask Run(Unit unit, G2M_ExitGameRequest request, M2G_ExitGameResponse response, Action reply)
        {
            //TODO:保存玩家数据到数据库，执行相关下线操作

            reply();

            //正式释放Unit
            await unit.RemoveLocation();
            UnitComponent unitComponent = unit.DomainScene().GetComponent<UnitComponent>();
            unitComponent.Remove(unit.Id);
        }
    }
}
