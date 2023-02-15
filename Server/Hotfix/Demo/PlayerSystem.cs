namespace ET
{
    [FriendClass(typeof(Player))]
    public static class PlayerSystem
    {
        [ObjectSystem]
        public class PlayerAwakeSystem1 : AwakeSystem<Player, string>
        {
            public override void Awake(Player self, string a)
            {
                self.Account = a.GetHashCode();
            }
        }

        public class PlayerAwakeSystem2 : AwakeSystem<Player, long, long>
        {
            public override void Awake(Player self, long a, long b)
            {
                self.Account = a;
                self.UnitId = b;
            }
        }
    }
}