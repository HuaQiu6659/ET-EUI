
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

		public void DestroyWidget()
		{
			this.m_EButton_DecidedButton = null;
			this.m_EButton_DecidedImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_EButton_DecidedButton = null;
		private UnityEngine.UI.Image m_EButton_DecidedImage = null;
		public Transform uiTransform = null;
	}
}
