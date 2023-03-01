
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
		    		this.m_E_ReturnButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"title/E_Return");
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
		    		this.m_E_ReturnImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"title/E_Return");
     			}
     			return this.m_E_ReturnImage;
     		}
     	}

		public UnityEngine.UI.InputField E_AccountInputField
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountInputField == null )
     			{
		    		this.m_E_AccountInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_Account");
     			}
     			return this.m_E_AccountInputField;
     		}
     	}

		public UnityEngine.UI.Image E_AccountImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_AccountImage == null )
     			{
		    		this.m_E_AccountImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Account");
     			}
     			return this.m_E_AccountImage;
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
		    		this.m_E_PasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_Password");
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
		    		this.m_E_PasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Password");
     			}
     			return this.m_E_PasswordImage;
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
		    		this.m_E_RePasswordInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_RePassword");
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
		    		this.m_E_RePasswordImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_RePassword");
     			}
     			return this.m_E_RePasswordImage;
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
		    		this.m_E_EMailInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_EMail");
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
		    		this.m_E_EMailImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_EMail");
     			}
     			return this.m_E_EMailImage;
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
		    		this.m_E_VerificationInputField = UIFindHelper.FindDeepChild<UnityEngine.UI.InputField>(this.uiTransform.gameObject,"E_Verification");
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
		    		this.m_E_VerificationImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Verification");
     			}
     			return this.m_E_VerificationImage;
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
		    		this.m_E_SendVerificationButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_SendVerification");
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
		    		this.m_E_SendVerificationImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_SendVerification");
     			}
     			return this.m_E_SendVerificationImage;
     		}
     	}

		public UnityEngine.UI.Button E_RegistButton
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RegistButton == null )
     			{
		    		this.m_E_RegistButton = UIFindHelper.FindDeepChild<UnityEngine.UI.Button>(this.uiTransform.gameObject,"E_Regist");
     			}
     			return this.m_E_RegistButton;
     		}
     	}

		public UnityEngine.UI.Image E_RegistImage
     	{
     		get
     		{
     			if (this.uiTransform == null)
     			{
     				Log.Error("uiTransform is null.");
     				return null;
     			}
     			if( this.m_E_RegistImage == null )
     			{
		    		this.m_E_RegistImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_Regist");
     			}
     			return this.m_E_RegistImage;
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
		    		this.m_E_PasswordVisitableToggle = UIFindHelper.FindDeepChild<UnityEngine.UI.Toggle>(this.uiTransform.gameObject,"E_PasswordVisitable");
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
		    		this.m_E_PasswordVisitableBackgroundImage = UIFindHelper.FindDeepChild<UnityEngine.UI.Image>(this.uiTransform.gameObject,"E_PasswordVisitable/E_PasswordVisitableBackground");
     			}
     			return this.m_E_PasswordVisitableBackgroundImage;
     		}
     	}

		public void DestroyWidget()
		{
			this.m_E_ReturnButton = null;
			this.m_E_ReturnImage = null;
			this.m_E_AccountInputField = null;
			this.m_E_AccountImage = null;
			this.m_E_PasswordInputField = null;
			this.m_E_PasswordImage = null;
			this.m_E_RePasswordInputField = null;
			this.m_E_RePasswordImage = null;
			this.m_E_EMailInputField = null;
			this.m_E_EMailImage = null;
			this.m_E_VerificationInputField = null;
			this.m_E_VerificationImage = null;
			this.m_E_SendVerificationButton = null;
			this.m_E_SendVerificationImage = null;
			this.m_E_RegistButton = null;
			this.m_E_RegistImage = null;
			this.m_E_PasswordVisitableToggle = null;
			this.m_E_PasswordVisitableBackgroundImage = null;
			this.uiTransform = null;
		}

		private UnityEngine.UI.Button m_E_ReturnButton = null;
		private UnityEngine.UI.Image m_E_ReturnImage = null;
		private UnityEngine.UI.InputField m_E_AccountInputField = null;
		private UnityEngine.UI.Image m_E_AccountImage = null;
		private UnityEngine.UI.InputField m_E_PasswordInputField = null;
		private UnityEngine.UI.Image m_E_PasswordImage = null;
		private UnityEngine.UI.InputField m_E_RePasswordInputField = null;
		private UnityEngine.UI.Image m_E_RePasswordImage = null;
		private UnityEngine.UI.InputField m_E_EMailInputField = null;
		private UnityEngine.UI.Image m_E_EMailImage = null;
		private UnityEngine.UI.InputField m_E_VerificationInputField = null;
		private UnityEngine.UI.Image m_E_VerificationImage = null;
		private UnityEngine.UI.Button m_E_SendVerificationButton = null;
		private UnityEngine.UI.Image m_E_SendVerificationImage = null;
		private UnityEngine.UI.Button m_E_RegistButton = null;
		private UnityEngine.UI.Image m_E_RegistImage = null;
		private UnityEngine.UI.Toggle m_E_PasswordVisitableToggle = null;
		private UnityEngine.UI.Image m_E_PasswordVisitableBackgroundImage = null;
		public Transform uiTransform = null;
	}
}
