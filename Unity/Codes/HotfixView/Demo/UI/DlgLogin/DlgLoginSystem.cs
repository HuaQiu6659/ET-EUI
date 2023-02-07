using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
	public static  class DlgLoginSystem
	{
		public static void RegisterUIEvent(this DlgLogin self)
		{
			self.View.E_LoginButton.AddListenerAsync(self.OnLoginClickHandler);
			self.View.E_AccountInputField.onValueChanged.AddListener(input => self.View.E_PasswordInputField.text = string.Empty);
		}

		public static void ShowWindow(this DlgLogin self, Entity contextData = null)
		{
#if UNITY_EDITOR
			self.View.E_AccountInputField.text = UnityEditor.EditorPrefs.GetString("Account", "TestAccount");
			self.View.E_PasswordInputField.text = UnityEditor.EditorPrefs.GetString("Password", "TestPassword");
#endif
        }
		
		public static async ETTask OnLoginClickHandler(this DlgLogin self)
		{
			try
			{
				string account = self.View.E_AccountInputField.text;
				string password = self.View.E_PasswordInputField.text;
                int error = await LoginHelper.Login(
                    self.DomainScene(),
                    ConstValue.LoginAddress,
                    account,
                    password);

				if (error != ErrorCode.ERR_Success)
				{
					Log.Error(error.ToString());
					return;
				}

				//TODO:显示登录成功后的逻辑
				var uiCmp = self.DomainScene().GetComponent<UIComponent>();
                uiCmp.HideWindow(WindowID.WindowID_Login);
                uiCmp.ShowWindow(WindowID.WindowID_ServerList);

#if UNITY_EDITOR
				UnityEditor.EditorPrefs.SetString("Account", account);
				UnityEditor.EditorPrefs.SetString("Password", password);
#endif

			}
			catch (Exception e)
			{
				Log.Error(e.Message);
			}
		}
		
		public static void HideWindow(this DlgLogin self)
		{

		}
		
	}
}
