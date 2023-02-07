
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ObjectSystem]
	public class DlgTestsssViewComponentAwakeSystem : AwakeSystem<DlgTestsssViewComponent> 
	{
		public override void Awake(DlgTestsssViewComponent self)
		{
			self.uiTransform = self.GetParent<UIBaseWindow>().uiTransform;
		}
	}


	[ObjectSystem]
	public class DlgTestsssViewComponentDestroySystem : DestroySystem<DlgTestsssViewComponent> 
	{
		public override void Destroy(DlgTestsssViewComponent self)
		{
			self.DestroyWidget();
		}
	}
}
