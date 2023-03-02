namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgMessageError :Entity,IAwake,IUILogic
	{

		public DlgMessageErrorViewComponent View { get => this.Parent.GetComponent<DlgMessageErrorViewComponent>();} 

		 

	}
}
