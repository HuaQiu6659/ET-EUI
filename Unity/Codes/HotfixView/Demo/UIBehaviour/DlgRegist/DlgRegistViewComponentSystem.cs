
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgRegistViewComponentAwakeSystem : AwakeSystem<DlgRegistViewComponent> 
	{
		public override void Awake(DlgRegistViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgRegistViewComponentDestroySystem : DestroySystem<DlgRegistViewComponent> 
	{
		public override void Destroy(DlgRegistViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
