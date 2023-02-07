using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class ServerInfoManagerComponent : Entity, IAwake, ILoad, IDestroy
    {
        public List<ServerInfo> serverInfos = new List<ServerInfo>();
    }
}
