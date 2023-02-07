namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgTestsss :Entity,IAwake,IUILogic
	{

		public DlgTestsssViewComponent View { get => this.Parent.GetComponent<DlgTestsssViewComponent>();} 

		 

	}
}
