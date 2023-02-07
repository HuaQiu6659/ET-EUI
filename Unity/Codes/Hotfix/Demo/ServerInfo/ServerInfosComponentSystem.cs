using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(ServerInfosComponent))]
    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo info)
        {
            self.serverInfos.Add(info);
        }

        public static void Add(this ServerInfosComponent self, List<ServerInfoProto> infoProtos)
        {
            foreach (var info in infoProtos)
            {
                var result = new ServerInfo();
                result.FromMessage(info);
                self.serverInfos.Add(result);
            }
        }
    }

    [FriendClass(typeof(ServerInfosComponent))]
    public class ServerInfosComponentAwakeSystem : AwakeSystem<ServerInfosComponent>
    {
        public override void Awake(ServerInfosComponent self)
        {
            self.serverInfos.ClearEntityList();
        }
    }

    [FriendClass(typeof(ServerInfosComponent))]
    public class ServerInfosComponentDestroySystem : DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            self.serverInfos.ClearEntityList();
        }
    }
}
