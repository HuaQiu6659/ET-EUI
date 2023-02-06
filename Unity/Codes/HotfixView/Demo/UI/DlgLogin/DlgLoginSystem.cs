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
		}

		public static void ShowWindow(this DlgLogin self, Entity contextData = null)
		{
			
		}
		
		public static async ETTask OnLoginClickHandler(this DlgLogin self)
		{
			try
			{
                int error = await LoginHelper.Login(
                    self.DomainScene(),
                    ConstValue.LoginAddress,
                    self.View.E_AccountInputField.GetComponent<InputField>().text,
                    self.View.E_PasswordInputField.GetComponent<InputField>().text);

				if (error != ErrorCode.ERR_Success)
				{
					Log.Error(error.ToString());
					return;
				}

				//TODO:显示登录成功后的逻辑
				var uiCmp = self.DomainScene().GetComponent<UIComponent>();
                uiCmp.HideWindow(WindowID.WindowID_Login);
                uiCmp.ShowWindow(WindowID.WindowID_Lobby);
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
