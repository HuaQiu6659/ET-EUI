namespace ET
{
    /// <summary>
    /// 对登录后长时间无操作的账号进行下线处理
    /// </summary>
    [ComponentOf(typeof(Session))]
    public class AccountCheckOutTimeComponent : Entity, IAwake<long>, IDestroy
    {
        public long timer = 0;
        public long accountId = 0;
    }
}
