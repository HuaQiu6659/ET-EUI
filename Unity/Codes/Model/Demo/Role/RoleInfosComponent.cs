using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene)), ChildType(typeof(RoleInfo))]
    public class RoleInfosComponent : Entity, IAwake, IDestroy
    {
        public List<RoleInfo> roles = new List<RoleInfo>();
        public long currentRoleId = 0;
    }
}
