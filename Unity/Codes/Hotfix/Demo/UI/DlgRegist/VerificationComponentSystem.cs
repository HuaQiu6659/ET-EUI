using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClassAttribute(typeof(ET.VerificationComponent))]
    public static class VerificationComponentSystem
    {
        public static int GetInterval(this VerificationComponent self) => (DateTime.Now - self.lastClickTime).Seconds;

        public class VerificationComponentAwakeSystem : AwakeSystem<VerificationComponent>
        {
            public override void Awake(VerificationComponent self)
            {
                self.lastClickTime = new DateTime(2000, 1, 1);
            }
        }
    }
}
