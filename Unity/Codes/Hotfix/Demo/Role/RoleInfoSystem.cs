using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(RoleInfo))]
    public static class RoleInfoSystem
    {
        public static void FromMessage(this RoleInfo self, RoleInfoProto infoProto)
        {
            self.Id = infoProto.Id;
            self.name = infoProto.Name;
            self.serverId = infoProto.ServerId;
            self.status = (RoleStatus)infoProto.Status;
            self.accountId = infoProto.AccountId;
            self.lastLoginTime = infoProto.LastLoginTime;
            self.createTime = infoProto.CreateTime;
        }

        public static RoleInfoProto ToMessage(this RoleInfo self) => new RoleInfoProto()
        {
            Id = self.Id,
            Name = self.name,
            ServerId = self.serverId,
            Status = (int)self.status,
            AccountId = self.accountId,
            LastLoginTime = self.lastLoginTime,
            CreateTime = self.createTime
        };
    }
}
