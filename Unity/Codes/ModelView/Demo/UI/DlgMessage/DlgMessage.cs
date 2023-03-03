namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMessage :Entity,IAwake,IUILogic
	{

		public DlgMessageViewComponent View { get => this.Parent.GetComponent<DlgMessageViewComponent>();} 

		 

	}
}
