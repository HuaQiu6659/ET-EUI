using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    //接口放行
    public interface IUnitCache
    {

    }

    public class UnitCache : Entity, IAwake, IDestroy
    {
        public string key;

        //缓存数据
        public Dictionary<long , Entity> cacheComponentDictionary = new Dictionary<long , Entity>();
    }
}
