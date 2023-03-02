
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgMessageDefaultViewComponentAwakeSystem : AwakeSystem<DlgMessageDefaultViewComponent> 
	{
		public override void Awake(DlgMessageDefaultViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgMessageDefaultViewComponentDestroySystem : DestroySystem<DlgMessageDefaultViewComponent> 
	{
		public override void Destroy(DlgMessageDefaultViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
