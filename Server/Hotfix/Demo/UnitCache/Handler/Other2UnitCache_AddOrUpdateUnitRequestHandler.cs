using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Other2UnitCache_AddOrUpdateUnitRequestHandler : AMActorRpcHandler<Scene, Other2UnitCache_AddOrUpdateUnitRequest, UnitCache2Other_AddOrUpdateUnitResponse>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_AddOrUpdateUnitRequest request, UnitCache2Other_AddOrUpdateUnitResponse response, Action reply)
        {
            UnitCacheComponent component = scene.GetComponent<UnitCacheComponent>();
            using (ListComponent<Entity> entityList = ListComponent<Entity>.Create())
            {
                for (int i = 0; i < request.EnitityTypes.Count; i++)
                {
                    Type type = Game.EventSystem.GetType(request.EnitityTypes[i]);
                    Entity entity = MongoHelper.FromBson(type, request.EntityBytes[i]) as Entity;
                    entityList.Add(entity);
                }

                await component.AddOrUpdate(request.UnitId, entityList);
            }
        }
    }
}
