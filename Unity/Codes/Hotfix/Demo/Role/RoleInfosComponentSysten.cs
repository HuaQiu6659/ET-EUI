namespace ET
{
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class RoleInfosComponentSysten
    {
        public static void ClearRoles(this RoleInfosComponent self) => self.roles.Clear();

        public static bool GetCurrentRole(this RoleInfosComponent self, out RoleInfo role)
        {
            if (self.currentRoleId == RoleInfosComponent.UnSelectId || self.roles.Count < 1)
            {
                role = null;
                return false;
            }
            role = self.roles[self.currentRoleId];
            return true;
        }

        public static void RemoveRole(this RoleInfosComponent self, int roleIndex)
        {
            var target = self.roles[roleIndex];
            self.roles.Remove(target);
        }

        public static void RemoveRole(this RoleInfosComponent self, string roleName)
        {
            var target = self.roles.Find(r => r.name.Equals(roleName));
            self.roles.Remove(target);
        }
    }

    public class RoleInfosComponentDestroySystem : DestroySystem<RoleInfosComponent>
    {
        public override void Destroy(RoleInfosComponent self)
        {
            self.roles.ClearEntityList();
            self.currentRoleId = 0;
        }
    }
}
