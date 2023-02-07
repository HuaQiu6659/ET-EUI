namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgServerList :Entity,IAwake,IUILogic
	{

		public DlgServerListViewComponent View { get => this.Parent.GetComponent<DlgServerListViewComponent>();} 

		 

	}
}
