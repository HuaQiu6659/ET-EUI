using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class ServerInfo : Entity, IAwake
    {
        public ServerStatus status;
        public string serverName;

    }

    public enum ServerStatus
    {
        Normal,
        Maintain,   //维护
        Stop
    }
}
