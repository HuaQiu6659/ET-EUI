namespace ET
{
    [FriendClass(typeof(AccountSessionsComponent))]
    public static class AccountSessionsComponentSystem
    {
        /// <summary>
        /// 获取Session InstanceID
        /// </summary>
        /// <param name="accountId">Session所属账号</param>
        /// <param name="instanceId">Session InstanceID</param>
        /// <returns>是否获取成功</returns>
        public static bool Get(this AccountSessionsComponent self, long accountId, out long sessionInstanceId)
        {
            return self.accountSeesions.TryGetValue(accountId, out sessionInstanceId);
        }

        public static void AddOrSet(this AccountSessionsComponent self, long accountId, long sessionInstanceId)
        {
            if (self.accountSeesions.ContainsKey(accountId))
                self.accountSeesions[accountId] = sessionInstanceId;
            else
                self.accountSeesions.Add(accountId, sessionInstanceId);
        }

        public static void Remove(this AccountSessionsComponent self, long accountId)
        {
            if (self.accountSeesions.ContainsKey(accountId))
                self.accountSeesions.Remove(accountId);
        }
    }

    public class AccountSessionsComponentAwakeSystem : AwakeSystem<AccountSessionsComponent>
    {
        public override void Awake(AccountSessionsComponent self)
        {
            self.accountSeesions.Clear();
        }
    }

    public class AccountSessionsComponentDestroySystem : DestroySystem<AccountSessionsComponent>
    {
        public override void Destroy(AccountSessionsComponent self)
        {
            self.accountSeesions.Clear();
        }
    }
}
