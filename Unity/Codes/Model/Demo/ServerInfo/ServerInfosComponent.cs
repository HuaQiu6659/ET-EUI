using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class ServerInfosComponent : Entity, IAwake, IDestroy
    {
        public List<ServerInfo> serverInfos = new List<ServerInfo>();
    }
}
