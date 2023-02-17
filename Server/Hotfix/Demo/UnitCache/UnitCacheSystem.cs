namespace ET
{
    [FriendClassAttribute(typeof(ET.UnitCache))]
    public static class UnitCacheSystem
    {
        public static void AddOrUpdate(this UnitCache self, Entity entity)
        {
            if (entity is null)
                return;

            if (self.cacheComponentDictionary.TryGetValue(entity.Id, out Entity oldEntity))
            {
                if (!entity.Equals(oldEntity))
                    oldEntity.Dispose();

                self.cacheComponentDictionary.Remove(entity.Id);
            }

            self.cacheComponentDictionary.Add(entity.Id, entity);
        }

        public static async ETTask<Entity> Get(this UnitCache self, long unitId)
        {
            if (!self.cacheComponentDictionary.TryGetValue(unitId, out Entity entity))
            {
                entity = await DBManagerComponent.Instance.GetZoneDB(self.DomainZone()).Query<Entity>(unitId, self.key);
                if (entity != null)
                    self.AddOrUpdate(entity);
            }
            return entity;
        }

        public static void Delete(this UnitCache self, long unitId)
        {
            if (self.cacheComponentDictionary.TryGetValue(unitId, out Entity entity))
            {
                entity.Dispose();
                self.cacheComponentDictionary.Remove(unitId);
            }
        }

        public class UnitCacheDestroySystem : DestroySystem<UnitCache>
        {
            public override void Destroy(UnitCache self)
            {
                foreach (var value in self.cacheComponentDictionary.Values)
                {
                    value?.Dispose();
                }
                self.cacheComponentDictionary.Clear();
                self.key = null;
            }
        }
    }
}
