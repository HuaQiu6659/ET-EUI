
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMessageViewComponentAwakeSystem : AwakeSystem<DlgMessageViewComponent> 
	{
		public override void Awake(DlgMessageViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMessageViewComponentDestroySystem : DestroySystem<DlgMessageViewComponent> 
	{
		public override void Destroy(DlgMessageViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
