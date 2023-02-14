
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgRoleListViewComponentAwakeSystem : AwakeSystem<DlgRoleListViewComponent> 
	{
		public override void Awake(DlgRoleListViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRoleListViewComponentDestroySystem : DestroySystem<DlgRoleListViewComponent> 
	{
		public override void Destroy(DlgRoleListViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
