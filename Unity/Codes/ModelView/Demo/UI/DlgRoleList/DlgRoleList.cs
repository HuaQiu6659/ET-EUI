using System.Collections.Generic;
using UnityEngine.UI;

namespace ET
{
	 [ComponentOf(typeof(UIBaseWindow))]
	public  class DlgRoleList :Entity,IAwake,IUILogic
	{

		public DlgRoleListViewComponent View { get => this.Parent.GetComponent<DlgRoleListViewComponent>();}
		public ToggleGroup toggleGroup;
		public Dictionary<int, Scroll_Item_RoleToggle> rolesToggleDict;
	}
}
