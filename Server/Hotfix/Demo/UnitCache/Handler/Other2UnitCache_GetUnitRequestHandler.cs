using System;
using System.Collections.Generic;

namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCacheComponent))]
    public class Other2UnitCache_GetUnitRequestHandler : AMActorRpcHandler<Scene, Other2UnitCache_GetUnitRequest, UnitCache2Other_GetUnitResponse>
    {
        protected override async ETTask Run(Scene scene, Other2UnitCache_GetUnitRequest request, UnitCache2Other_GetUnitResponse response, Action reply)
        {
            UnitCacheComponent unitCacheComponent = scene.GetComponent<UnitCacheComponent>();
            Dictionary<string, Entity> dictionary = MonoPool.Instance.Fetch(typeof(Dictionary<string, Entity>)) as Dictionary<string, Entity>;

            try
            {
                if (request.ComponentNameList.Count == 0)
                {
                    dictionary.Add(nameof(Unit), null);

                    foreach (var key in unitCacheComponent.unitCacheKeyList)
                        dictionary.Add(key, null);
                }
                else
                    foreach (var name in request.ComponentNameList)
                        dictionary.Add(name, null);

                foreach (var key in dictionary.Keys)
                {
                    Entity entity = await unitCacheComponent.Get(request.UnitId, key);
                    dictionary[key] = entity;
                }

                response.ComponentNameList.AddRange(dictionary.Keys);
                response.EntityList.AddRange(dictionary.Values);
            }
            finally
            {
                dictionary.Clear();
                MonoPool.Instance.Recycle(dictionary);
            }

            reply();
        }
    }
}
