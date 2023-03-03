using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf]
    public class DlgParamsComponent : Entity, IAwake
    {
        public Dictionary<Type, IDictionary> objects;
    }
}
