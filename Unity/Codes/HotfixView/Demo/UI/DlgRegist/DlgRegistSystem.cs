using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgRegist))]
	public static  class DlgRegistSystem
	{
        #region ------------ 响应事件 ------------

        public static void RegisterUIEvent(this DlgRegist self)
        {
            var view = self.View;
            view.E_PasswordVisitableToggle.AddListener(self.OnPasswordVisitableToggleValueChanged);
            view.E_RePasswordInputField.onValueChanged.AddListener(self.OnRePasswordValueChanged);
            view.E_RegistButton.AddListener(self.OnRegistBtnClick);
            view.E_ReturnButton.AddListener(self.OnReturnBtnClick);
        }

        static void OnReturnBtnClick(this DlgRegist self)
        {
            var uiCmp = self.DomainScene().GetComponent<UIComponent>();
            uiCmp.HideWindow(WindowID.WindowID_Regist);
            uiCmp.ShowWindow(WindowID.WindowID_Login);
        }

        static void OnPasswordVisitableToggleValueChanged(this DlgRegist self, bool isOn)
        {
            var view = self.View;
            view.E_PasswordVisitableBackgroundImage.enabled = !isOn;
            var type = isOn ? InputField.ContentType.Standard : InputField.ContentType.Password;
            view.E_PasswordInputField.contentType = view.E_RePasswordInputField.contentType = type;
            view.E_PasswordInputField.ForceLabelUpdate();
            view.E_RePasswordInputField.ForceLabelUpdate();
        }

        static void OnRePasswordValueChanged(this DlgRegist self, string input)
        {
            var view = self.View;
            bool isEquals = input.Length >= 6 && input.Equals(view.E_RePasswordInputField.text);
            view.E_RegistButton.interactable = isEquals && view.E_AccountInputField.text.Length >= 6; 
        }

        static async void OnRegistBtnClick(this DlgRegist self)
        {
            var view = self.View;
            view.E_RegistButton.interactable = false;
            int result = await LoginHelper.Regist(self.ZoneScene(), view.E_AccountInputField.text, view.E_PasswordInputField.text, view.E_EMailInputField.text);
            view.E_RegistButton.interactable = true;

            var uiCmp = self.DomainScene().GetComponent<UIComponent>();

            DlgHelper.message = MessageHelper.GetMessage(result);
            uiCmp.ShowWindow(WindowID.WindowID_Helper);
        }

        #endregion

        public static void ShowWindow(this DlgRegist self, Entity contextData = null)
		{
            var view = self.View;
            view.E_AccountInputField.text =
                view.E_PasswordInputField.text =
                view.E_RePasswordInputField.text = string.Empty;
        }
	}
}
