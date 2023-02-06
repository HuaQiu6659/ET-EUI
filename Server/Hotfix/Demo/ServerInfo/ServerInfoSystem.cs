using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(ServerInfo))]
    public static class ServerInfoSystem
    {
        public static void FromMessage(this ServerInfo self, ServerInfoProto infoProto)
        {
            self.status = (ServerStatus)infoProto.Status;
            self.serverName = infoProto.ServerName;
            self.Id = infoProto.Id;
        }

        public static ServerInfoProto ToMessage(this ServerInfo self)
        {
            return new ServerInfoProto()
            {
                Id = self.Id,
                ServerName = self.serverName,
                Status = (int)self.status
            };
        }
    }
}
