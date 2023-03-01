using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf]
    public class VerificationComponent :Entity, IAwake
    {
        public string verification;
        public DateTime lastClickTime;
    }
}
