using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class AccountInfoComponentSystem
    {
        //public static 
    }

    public class AccountInfoComponentSystemAwakeSystem : AwakeSystem<AccountInfoComponent>
    {
        public override void Awake(AccountInfoComponent self)
        {

        }
    }
    public class AccountInfoComponentSystemDestroySystem : DestroySystem<AccountInfoComponent>
    {
        public override void Destroy(AccountInfoComponent self)
        {
            self.token = default;
            self.accountId = 0;
        }
    }
}
