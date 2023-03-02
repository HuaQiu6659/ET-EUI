
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMessageErrorViewComponentAwakeSystem : AwakeSystem<DlgMessageErrorViewComponent> 
	{
		public override void Awake(DlgMessageErrorViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMessageErrorViewComponentDestroySystem : DestroySystem<DlgMessageErrorViewComponent> 
	{
		public override void Destroy(DlgMessageErrorViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
