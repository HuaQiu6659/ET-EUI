namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public class DlgRegist :Entity,IAwake,IUILogic
	{

		public DlgRegistViewComponent View { get => this.Parent.GetComponent<DlgRegistViewComponent>();} 

		 

	}
}
