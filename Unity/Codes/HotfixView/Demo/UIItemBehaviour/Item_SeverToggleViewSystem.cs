
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class Scroll_Item_SeverToggleDestroySystem : DestroySystem<Scroll_Item_SeverToggle> 
	{
		public override void Destroy( Scroll_Item_SeverToggle self )
		{
			self.DestroyWidget();
		}
	}
}
