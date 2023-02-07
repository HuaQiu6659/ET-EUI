namespace ET
{
    public static class RoleInfosComponentSysten
    {

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
