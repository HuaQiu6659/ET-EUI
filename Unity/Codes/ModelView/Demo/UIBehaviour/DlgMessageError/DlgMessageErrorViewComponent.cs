
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgMessageErrorViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Text E_MessageText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_MessageText == null )
     			{
		    		this.m_E_MessageText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Message");
     			}
     			return this.m_E_MessageText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_MessageText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Text m_E_MessageText = null;
		public Transform uiTransform = null;
	}
}
