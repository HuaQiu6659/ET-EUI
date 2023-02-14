
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgServerListViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button EButton_DecidedButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DecidedButton == null )
     			{
		    		this.m_EButton_DecidedButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"EButton_Decided");
     			}
     			return this.m_EButton_DecidedButton;
     		}
     	}

		public UnityEngine.UI.Image EButton_DecidedImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_EButton_DecidedImage == null )
     			{
		    		this.m_EButton_DecidedImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"EButton_Decided");
     			}
     			return this.m_EButton_DecidedImage;
     		}
     	}

		public UnityEngine.UI.LoopVerticalScrollRect ELoopScrollList_ServerlistLoopVerticalScrollRect
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELoopScrollList_ServerlistLoopVerticalScrollRect == null )
     			{
		    		this.m_ELoopScrollList_ServerlistLoopVerticalScrollRect = UIFindHelper.FindDeepChild<UnityEngine.UI.LoopVerticalScrollRect>(this.uiTransform.gameObject,"ELoopScrollList_Serverlist");
     			}
     			return this.m_ELoopScrollList_ServerlistLoopVerticalScrollRect;
     		}
     	}

		public UnityEngine.UI.Text ELabel_SelectServerText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_ELabel_SelectServerText == null )
     			{
		    		this.m_ELabel_SelectServerText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"ELabel_SelectServer");
     			}
     			return this.m_ELabel_SelectServerText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_EButton_DecidedButton = null;
			this.m_EButton_DecidedImage = null;
			this.m_ELoopScrollList_ServerlistLoopVerticalScrollRect = null;
			this.m_ELabel_SelectServerText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_DecidedButton = null;
		private UnityEngine.UI.Image m_EButton_DecidedImage = null;
		private UnityEngine.UI.LoopVerticalScrollRect m_ELoopScrollList_ServerlistLoopVerticalScrollRect = null;
		private UnityEngine.UI.Text m_ELabel_SelectServerText = null;
		public Transform uiTransform = null;
	}
}
