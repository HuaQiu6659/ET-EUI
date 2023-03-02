namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMessageDefault :Entity,IAwake,IUILogic
	{

		public DlgMessageDefaultViewComponent View { get => this.Parent.GetComponent<DlgMessageDefaultViewComponent>();} 

		 

	}
}
