
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_RoleToggleDestroySystem : DestroySystem<Scroll_Item_RoleToggle> 
	{
		public override void Destroy( Scroll_Item_RoleToggle self )
		{
			self.DestroyWidget();
		}
	}
}
