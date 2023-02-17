namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCacheComponent))]
    [FriendClassAttribute(typeof(ET.UnitCache))]
    public static class UnitCacheComponentSystem
    {
        public static async ETTask AddOrUpdate(this UnitCacheComponent self, long unitId, ListComponent<Entity> entities)
        {
            using (ListComponent<Entity> list = ListComponent<Entity>.Create())
            {
                foreach (var entity in entities)
                {
                    string key = entity.GetType().Name;
                    if (!self.unitCaches.TryGetValue(key, out UnitCache cache))
                    {
                        cache = self.AddChild<UnitCache>();
                        cache.key = key;
                        self.unitCaches.Add(key, cache);
                    }
                    cache.AddOrUpdate(entity);
                }

                if (list.Count > 0)
                    await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Save(unitId, list);
            }
        }
        
        public static async ETTask<Entity> Get(this UnitCacheComponent self, long id, string key)
        {
            if (!self.unitCaches.TryGetValue(key, out UnitCache unitCache))
            {
                unitCache = self.AddChild<UnitCache>();
                unitCache.key = key;
                self.unitCaches.Add(key, unitCache);
            }
            return await unitCache.Get(id);
        }

        public static void Delete(this UnitCacheComponent self, long unitId)
        {
            foreach (var cache in self.unitCaches.Values)
            {
                cache.Delete(unitId);
            }
        }
        
        [FriendClassAttribute(typeof(ET.UnitCache))]
        public class UnitCacheComponentAwakeSystem : AwakeSystem<UnitCacheComponent>
        {
            public override void Awake(UnitCacheComponent self)
            {
                self.unitCaches.Clear();
                foreach (System.Type type in Game.EventSystem.GetTypes().Values)
                    if (type is IUnitCache)
                    {
                        self.unitCacheKeyList.Add(type.Name);
                        UnitCache unitCache = self.AddChild<UnitCache>();
                        unitCache.key = type.Name;
                        self.unitCaches.Add(type.Name, unitCache);
                    }
            }
        }

        public class UnitCacheComponentDestroySystem : DestroySystem<UnitCacheComponent>
        {
            public override void Destroy(UnitCacheComponent self)
            {
                foreach (var cache in self.unitCaches.Values)
                {
                    cache?.Dispose();
                }
                self.unitCaches.Clear();
                self.unitCacheKeyList.Clear();
            }
        }
    }
}
