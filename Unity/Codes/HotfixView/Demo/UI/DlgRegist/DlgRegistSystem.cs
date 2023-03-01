using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Networking.UnityWebRequest;

namespace ET
{
    [FriendClass(typeof(DlgRegist))]
    [FriendClassAttribute(typeof(ET.VerificationComponent))]
    public static class DlgRegistSystem
    {
        #region ------------ 响应事件 ------------

        public static void RegisterUIEvent(this DlgRegist self)
        {
            var view = self.View;
            view.E_PasswordVisitableToggle.AddListener(self.OnPasswordVisitableToggleValueChanged);
            view.E_RePasswordInputField.onValueChanged.AddListener(self.OnRePasswordValueChanged);
            view.E_RegistButton.AddListener(self.OnRegistBtnClick);
            view.E_ReturnButton.AddListener(self.OnReturnBtnClick);
            view.E_SendVerificationButton.AddListener(self.OnSendVerificationButtonClick);
            //view.E_EMailInputField.onValueChanged.AddListener(self.OnEMailChanged);
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

        static async void OnSendVerificationButtonClick(this DlgRegist self)
        {
            var zoneScene = self.ZoneScene();
            var uiCmp = zoneScene.GetComponent<UIComponent>();

            var view = self.View;
            view.E_SendVerificationButton.interactable = false;
            A2C_Verification response = await LoginHelper.SendRegistVerification(zoneScene, view.E_EMailInputField.text);
            if (response is null)
            {
                DlgHelper.message = "网络错误，获取验证码失败";
                uiCmp.ShowWindow(WindowID.WindowID_Helper);
            }
            else if (response.Error != 0)
            {
                DlgHelper.message = MessageHelper.GetMessage(response.Error);
                uiCmp.ShowWindow(WindowID.WindowID_Helper);
            }
            else
            {
                DlgHelper.message = "成功获取验证码，请查看邮箱。";
                uiCmp.ShowWindow(WindowID.WindowID_Helper);

                var verificationCmp = self.GetComponent<VerificationComponent>();
                verificationCmp.verification = response.Verification;
                verificationCmp.lastClickTime = DateTime.Now;

                //TODO:按钮倒计时

                await TimerComponent.Instance.WaitAsync(6000);
            }
            view.E_SendVerificationButton.interactable = true;
        }

        static void OnEMailChanged(this DlgRegist self, string input)
        {
            var view = self.View;

            if (self.GetComponent<VerificationComponent>().GetInterval() < 60)
            {
                view.E_SendVerificationButton.interactable = false;
                return;
            }

            /*if (view.E_SendVerificationButton.interactable)
                view.E_SendVerificationButton.interactable = StringHelper.IsEmail(input);*/
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

        public class DlgRegistAwakeSystem : AwakeSystem<DlgRegist>
        {
            public override void Awake(DlgRegist self)
            {
                self.AddComponent<VerificationComponent>();
            }
        }
    }
}
