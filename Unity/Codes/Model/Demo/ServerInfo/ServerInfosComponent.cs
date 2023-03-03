using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class ServerInfosComponent : Entity, IAwake, IDestroy
    {
        public List<ServerInfo> servers;

        public int currentServerId = UnSelectId;

        public const int UnSelectId = -1;
    }
}
