namespace ET
{
    [FriendClass(typeof(LoginInfoRecordComponent))]
    public static class LoginInfoRecordComponentSystem
    {
        public static void AddOrSet(this LoginInfoRecordComponent self, long id, int value)
        {
            if (self.accountLoginInfos.ContainsKey(id))
                self.accountLoginInfos[id] = value;
            else
                self.accountLoginInfos.Add(id, value);
        }

        public static void Remove(this LoginInfoRecordComponent self, long id) => self.accountLoginInfos.Remove(id);

        public static bool Get(this LoginInfoRecordComponent self, long id, out int value) => self.accountLoginInfos.TryGetValue(id, out value);

        public static bool IsExist(this LoginInfoRecordComponent self, long id) => self.accountLoginInfos.ContainsKey(id);
    }

    public class LoginInfoRecordComponentDestroySystem : DestroySystem<LoginInfoRecordComponent>
    {
        public override void Destroy(LoginInfoRecordComponent self)
        {
            self.accountLoginInfos.Clear();
        }
    }
}
