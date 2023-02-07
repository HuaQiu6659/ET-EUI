namespace ET
{
    [FriendClass(typeof(ServerInfoManagerComponent))]
    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            //从数据库中获取区服信息

            await ETTask.CompletedTask;
        }
    }

    [FriendClass(typeof(ServerInfoManagerComponent))]
    public class ServerInfoManagerComponentAwakeSystem : AwakeSystem<ServerInfoManagerComponent>
    {
        public override void Awake(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    [FriendClass(typeof(ServerInfoManagerComponent))]
    public class ServerInfoManagerComponentLoadSystem : LoadSystem<ServerInfoManagerComponent>
    {
        public override void Load(ServerInfoManagerComponent self)
        {
            self.Awake().Coroutine();
        }
    }

    [FriendClass(typeof(ServerInfoManagerComponent))]
    public class ServerInfoManagerComponentDestroySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            self.serverInfos.RemoveAll(info => info is null);
            foreach (var info in self.serverInfos)
                info.Dispose();

            self.serverInfos.Clear();
        }
    }
}
