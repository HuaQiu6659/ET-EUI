using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ET
{
	public static class DlgLoginSystem
	{
        #region ------------- 响应事件 -------------

        public static void RegisterUIEvent(this DlgLogin self)
        {
            var view = self.View;
            view.E_LoginButton.AddListenerAsync(self.OnLoginClickHandler);
            view.E_AccountInputField.onValueChanged.AddListener(input => self.View.E_PasswordInputField.text = string.Empty);
            view.E_PasswordInputField.onValueChanged.AddListener(self.OnPasswordInput);
            view.E_PasswordVisitableToggle.AddListener(self.OnPasswordVisitableToggleValueChanged);
            view.E_RegistButton.AddListener(self.OnRegistBtnClick);
            view.E_ForgotButton.AddListener(self.OnRegistBtnClick);
        }

        static async ETTask OnLoginClickHandler(this DlgLogin self)
        {
            var view = self.View;

            var loginBtn = view.E_LoginButton;
            loginBtn.SetUntouchable();

            try
            {
                string account = view.E_AccountInputField.text;
                string password = view.E_PasswordInputField.text;
                int error = await LoginHelper.Login(
                    self.DomainScene(),
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

                PlayerPrefs.SetString("Account", account);
                PlayerPrefs.SetString("Password", password);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            finally
            {
                loginBtn.SetTouchable();
            }
        }

        static void OnPasswordVisitableToggleValueChanged(this DlgLogin self, bool isOn)
        {
            var view = self.View;
            view.E_PasswordVisitableBackgroundImage.enabled = !isOn;
            view.E_PasswordInputField.contentType = isOn ? InputField.ContentType.Standard : InputField.ContentType.Password;
            view.E_PasswordInputField.ForceLabelUpdate();
        }

        static void OnPasswordInput(this DlgLogin self, string input)
        {
            var view = self.View;
            bool isEmail = StringHelper.IsEmail(view.E_AccountInputField.text);
            view.E_LoginButton.interactable = input.Length >= 6 && input.Length <= 20 && isEmail;
        }

        static void OnRegistBtnClick(this DlgLogin self)
        {
            var uiCmp = self.DomainScene().GetComponent<UIComponent>();
            uiCmp.HideWindow(WindowID.WindowID_Login);
            uiCmp.ShowWindow(WindowID.WindowID_Regist);
        }

        #endregion

        public static void ShowWindow(this DlgLogin self, Entity contextData = null)
        {
			var view = self.View;
            view.E_AccountInputField.text = PlayerPrefs.GetString("Account", default);
            view.E_PasswordInputField.text = PlayerPrefs.GetString("Password", default);
        }
		
		public static void HideWindow(this DlgLogin self)
		{

		}
	}

	public class DlgLoginAwakeSystem : AwakeSystem<DlgLogin>
	{
		public override void Awake(DlgLogin self) { }
    }
}
