
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_ServerToggleDestroySystem : DestroySystem<Scroll_Item_ServerToggle> 
	{
		public override void Destroy( Scroll_Item_ServerToggle self )
		{
			self.DestroyWidget();
		}
	}
}
