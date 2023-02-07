using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class RoleInfo : Entity, IAwake, IDestroy
    {
        public string name;
        public int serverId;
        public RoleStatus status;
        public long accountId;
        public long lastLoginTime;
        public long createTime;
    }

    public enum RoleStatus
    {
        Normal,
        Freeze
    }
}
