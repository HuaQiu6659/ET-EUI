
using UnityEngine;
using UnityEngine.UI;
namespace ET
{
	[ComponentOf(typeof(UIBaseWindow))]
	[EnableMethod]
	public  class DlgRegistViewComponent : Entity,IAwake,IDestroy 
	{
		public UnityEngine.UI.Button E_ReturnButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReturnButton == null )
     			{
		    		this.m_E_ReturnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"Popup_TitleBar/E_Return");
     			}
     			return this.m_E_ReturnButton;
     		}
     	}

		public UnityEngine.UI.Image E_ReturnImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_ReturnImage == null )
     			{
		    		this.m_E_ReturnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"Popup_TitleBar/E_Return");
     			}
     			return this.m_E_ReturnImage;
     		}
     	}

		public UnityEngine.UI.Text E_TitleText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_TitleText == null )
     			{
		    		this.m_E_TitleText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"Popup_TitleBar/E_Title");
     			}
     			return this.m_E_TitleText;
     		}
     	}

		public UnityEngine.UI.InputField E_EMailInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EMailInputField == null )
     			{
		    		this.m_E_EMailInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"InputFields/E_EMail");
     			}
     			return this.m_E_EMailInputField;
     		}
     	}

		public UnityEngine.UI.Image E_EMailImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_EMailImage == null )
     			{
		    		this.m_E_EMailImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_EMail");
     			}
     			return this.m_E_EMailImage;
     		}
     	}

		public UnityEngine.UI.Button E_SendVerificationButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SendVerificationButton == null )
     			{
		    		this.m_E_SendVerificationButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"InputFields/E_EMail/E_SendVerification");
     			}
     			return this.m_E_SendVerificationButton;
     		}
     	}

		public UnityEngine.UI.Image E_SendVerificationImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SendVerificationImage == null )
     			{
		    		this.m_E_SendVerificationImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_EMail/E_SendVerification");
     			}
     			return this.m_E_SendVerificationImage;
     		}
     	}

		public UnityEngine.UI.Text E_CountdownText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_CountdownText == null )
     			{
		    		this.m_E_CountdownText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"InputFields/E_EMail/E_SendVerification/E_Countdown");
     			}
     			return this.m_E_CountdownText;
     		}
     	}

		public UnityEngine.UI.InputField E_VerificationInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_VerificationInputField == null )
     			{
		    		this.m_E_VerificationInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"InputFields/E_Verification");
     			}
     			return this.m_E_VerificationInputField;
     		}
     	}

		public UnityEngine.UI.Image E_VerificationImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_VerificationImage == null )
     			{
		    		this.m_E_VerificationImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_Verification");
     			}
     			return this.m_E_VerificationImage;
     		}
     	}

		public UnityEngine.UI.InputField E_PasswordInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordInputField == null )
     			{
		    		this.m_E_PasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"InputFields/E_Password");
     			}
     			return this.m_E_PasswordInputField;
     		}
     	}

		public UnityEngine.UI.Image E_PasswordImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordImage == null )
     			{
		    		this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_Password");
     			}
     			return this.m_E_PasswordImage;
     		}
     	}

		public UnityEngine.UI.Toggle E_PasswordVisitableToggle
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordVisitableToggle == null )
     			{
		    		this.m_E_PasswordVisitableToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"InputFields/E_Password/E_PasswordVisitable");
     			}
     			return this.m_E_PasswordVisitableToggle;
     		}
     	}

		public UnityEngine.UI.Image E_PasswordVisitableBackgroundImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_PasswordVisitableBackgroundImage == null )
     			{
		    		this.m_E_PasswordVisitableBackgroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_Password/E_PasswordVisitable/E_PasswordVisitableBackground");
     			}
     			return this.m_E_PasswordVisitableBackgroundImage;
     		}
     	}

		public UnityEngine.UI.InputField E_RePasswordInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RePasswordInputField == null )
     			{
		    		this.m_E_RePasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"InputFields/E_RePassword");
     			}
     			return this.m_E_RePasswordInputField;
     		}
     	}

		public UnityEngine.UI.Image E_RePasswordImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RePasswordImage == null )
     			{
		    		this.m_E_RePasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"InputFields/E_RePassword");
     			}
     			return this.m_E_RePasswordImage;
     		}
     	}

		public UnityEngine.UI.Image E_InfoImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_InfoImage == null )
     			{
		    		this.m_E_InfoImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Info");
     			}
     			return this.m_E_InfoImage;
     		}
     	}

		public UnityEngine.UI.Button E_LoginButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginButton == null )
     			{
		    		this.m_E_LoginButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Info/E_Login");
     			}
     			return this.m_E_LoginButton;
     		}
     	}

		public UnityEngine.UI.Text E_LoginText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_LoginText == null )
     			{
		    		this.m_E_LoginText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Info/E_Login");
     			}
     			return this.m_E_LoginText;
     		}
     	}

		public UnityEngine.UI.Button E_SubmitButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SubmitButton == null )
     			{
		    		this.m_E_SubmitButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Submit");
     			}
     			return this.m_E_SubmitButton;
     		}
     	}

		public UnityEngine.UI.Image E_SubmitImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SubmitImage == null )
     			{
		    		this.m_E_SubmitImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Submit");
     			}
     			return this.m_E_SubmitImage;
     		}
     	}

		public UnityEngine.UI.Text E_SubmitTextText
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_SubmitTextText == null )
     			{
		    		this.m_E_SubmitTextText = UIFindHelper.FindDeepChild<UnityEngine.UI.Text>(this.uiTransform.gameObject,"E_Submit/E_SubmitText");
     			}
     			return this.m_E_SubmitTextText;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ReturnButton = null;
			this.m_E_ReturnImage = null;
			this.m_E_TitleText = null;
			this.m_E_EMailInputField = null;
			this.m_E_EMailImage = null;
			this.m_E_SendVerificationButton = null;
			this.m_E_SendVerificationImage = null;
			this.m_E_CountdownText = null;
			this.m_E_VerificationInputField = null;
			this.m_E_VerificationImage = null;
			this.m_E_PasswordInputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_PasswordVisitableToggle = null;
			this.m_E_PasswordVisitableBackgroundImage = null;
			this.m_E_RePasswordInputField = null;
			this.m_E_RePasswordImage = null;
			this.m_E_InfoImage = null;
			this.m_E_LoginButton = null;
			this.m_E_LoginText = null;
			this.m_E_SubmitButton = null;
			this.m_E_SubmitImage = null;
			this.m_E_SubmitTextText = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ReturnButton = null;
		private UnityEngine.UI.Image m_E_ReturnImage = null;
		private UnityEngine.UI.Text m_E_TitleText = null;
		private UnityEngine.UI.InputField m_E_EMailInputField = null;
		private UnityEngine.UI.Image m_E_EMailImage = null;
		private UnityEngine.UI.Button m_E_SendVerificationButton = null;
		private UnityEngine.UI.Image m_E_SendVerificationImage = null;
		private UnityEngine.UI.Text m_E_CountdownText = null;
		private UnityEngine.UI.InputField m_E_VerificationInputField = null;
		private UnityEngine.UI.Image m_E_VerificationImage = null;
		private UnityEngine.UI.InputField m_E_PasswordInputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private UnityEngine.UI.Toggle m_E_PasswordVisitableToggle = null;
		private UnityEngine.UI.Image m_E_PasswordVisitableBackgroundImage = null;
		private UnityEngine.UI.InputField m_E_RePasswordInputField = null;
		private UnityEngine.UI.Image m_E_RePasswordImage = null;
		private UnityEngine.UI.Image m_E_InfoImage = null;
		private UnityEngine.UI.Button m_E_LoginButton = null;
		private UnityEngine.UI.Text m_E_LoginText = null;
		private UnityEngine.UI.Button m_E_SubmitButton = null;
		private UnityEngine.UI.Image m_E_SubmitImage = null;
		private UnityEngine.UI.Text m_E_SubmitTextText = null;
		public Transform uiTransform = null;
	}
}
