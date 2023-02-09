using System.Collections.Generic;
using UnityEngine.UI;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgServerList :Entity,IAwake,IUILogic
	{
		public DlgServerListViewComponent View { get => this.Parent.GetComponent<DlgServerListViewComponent>();}
		public Dictionary<int, Scroll_Item_SeverToggle> serverToggleDict;
		public ToggleGroup toggleGroup;
	}
}
