using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene)), ChildType(typeof(UnitCache))]
    public class UnitCacheComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<string, UnitCache> unitCaches = new Dictionary<string, UnitCache>();
        public List<string> unitCacheKeyList = new List<string>();
    }
}
