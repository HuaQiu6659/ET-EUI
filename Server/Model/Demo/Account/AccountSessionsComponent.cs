using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 已登录账号管理
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public class AccountSessionsComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<long, long> accountSeesions = new Dictionary<long, long>();
    }
}
