using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class EntityHelper
    {
        //释放、清空列表
        public static void ClearEntityList<T>(this List<T> self) where T : Entity
        {
            self.RemoveAll(x => x is null);
            foreach (var item in self)
                item.Dispose();

            self.Clear();
        }
    }
}
