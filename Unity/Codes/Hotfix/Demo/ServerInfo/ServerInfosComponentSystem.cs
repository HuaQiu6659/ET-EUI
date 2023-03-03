using System.Collections.Generic;

namespace ET
{
    [FriendClass(typeof(ServerInfosComponent))]
    public static class ServerInfosComponentSystem
    {
        public static void Add(this ServerInfosComponent self, ServerInfo info)
        {
            self.servers.Add(info);
        }

        public static void Add(this ServerInfosComponent self, List<ServerInfoProto> infoProtos)
        {
            foreach (var info in infoProtos)
            {
                var result = new ServerInfo();
                result.FromMessage(info);
                self.servers.Add(result);
            }
        }
    }

    [FriendClass(typeof(ServerInfosComponent))]
    public class ServerInfosComponentAwakeSystem : AwakeSystem<ServerInfosComponent>
    {
        public override void Awake(ServerInfosComponent self)
        {
            self.servers = new List<ServerInfo>();
        }
    }

    [FriendClass(typeof(ServerInfosComponent))]
    public class ServerInfosComponentDestroySystem : DestroySystem<ServerInfosComponent>
    {
        public override void Destroy(ServerInfosComponent self)
        {
            self.servers.ClearEntityList();
        }
    }
}
