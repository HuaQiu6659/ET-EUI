namespace ET
{
    [FriendClass(typeof(ServerInfoManagerComponent))
        , FriendClass(typeof(ServerInfo))]
    public static class ServerInfoManagerComponentSystem
    {
        public static async ETTask Awake(this ServerInfoManagerComponent self)
        {
            var db = DBManagerComponent.Instance.GetZoneDB(self.DomainZone());
            var serverInfosList = await db.Query<ServerInfo>(d => true/*返回所有*/);
            if (serverInfosList?.Count == 0)
            {
                Log.Error("No any ServerInfos are in database.");

                self.ClearInfos();
                //数据库不存在区服信息时读取Excel配置表，并向数据库导入信息
                var serverInfoConfig = ServerInfoConfigCategory.Instance.GetAll();
                foreach (var infoConfig in serverInfoConfig.Values)
                {
                    var newInfo = self.AddChildWithId<ServerInfo>(infoConfig.Id);
                    newInfo.serverName = infoConfig.ServerName;
                    newInfo.status = ServerStatus.Normal;
                    self.serverInfos.Add(newInfo);

                    await db.Save(newInfo); //Save会根据类名找到对应数据库（如果存在）并将数据传入该数据库
                }
                return;
            }

            self.ClearInfos();

            foreach (var info in serverInfosList)
            {
                self.AddChild(info);
                self.serverInfos.Add(info);
            }
        }

        public static void ClearInfos(this ServerInfoManagerComponent self)
        {
            self.serverInfos.RemoveAll(info => info is null);
            foreach (var info in self.serverInfos)
                info.Dispose();

            self.serverInfos.Clear();
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
    public class ServerInfoManagerComponentDestroySystem : DestroySystem<ServerInfoManagerComponent>
    {
        public override void Destroy(ServerInfoManagerComponent self)
        {
            self.ClearInfos();
        }
    }
}
