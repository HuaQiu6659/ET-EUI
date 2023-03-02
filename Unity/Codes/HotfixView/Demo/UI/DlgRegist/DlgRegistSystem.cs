using UnityEngine.UI;

namespace ET
{
    [FriendClass(typeof(DlgRegist))]
    [FriendClassAttribute(typeof(ET.DlgRegistTypeComponent))]
    public static class DlgRegistSystem
    {
        #region ------------ 响应事件 ------------

        public static void RegisterUIEvent(this DlgRegist self)
        {
            var view = self.View;
            view.E_PasswordVisitableToggle.AddListener(self.OnPasswordVisitableToggleValueChanged);
            view.E_SubmitButton.AddListener(self.OnSubmitBtnClick);
            view.E_ReturnButton.AddListener(self.OnReturnBtnClick);
            view.E_LoginButton.AddListener(self.OnReturnBtnClick);
            view.E_SendVerificationButton.AddListener(self.OnSendVerificationButtonClick);
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

        static async void OnSendVerificationButtonClick(this DlgRegist self)
        {
            var view = self.View;
            var zoneScene = self.ZoneScene();
            var uiCmp = zoneScene.GetComponent<UIComponent>();
            string email = view.E_EMailInputField.text;

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

                //按钮倒计时
                var countDownText = view.E_CountdownText;
                for (int i = 60; i > 0; i--)
                {
                    //改变邮箱时
                    if (!view.E_EMailInputField.text.Equals(email))
                        continue;

                    countDownText.text = $"验证({i}s)";
                    await TimerComponent.Instance.WaitAsync(1000);
                }
                countDownText.text = "验证";
            }
            view.E_SendVerificationButton.interactable = true;
        }

        static async void OnSubmitBtnClick(this DlgRegist self)
        {
            var view = self.View;
            var uiCmp = self.DomainScene().GetComponent<UIComponent>();

            if (!CheckInput(view.E_EMailInputField, "邮箱")
                || !CheckInput(view.E_PasswordInputField, "密码")
                || !CheckInput(view.E_RePasswordInputField, "密码")
                || !CheckInput(view.E_VerificationInputField, "验证码"))
                return;

            if (!StringHelper.IsEmail(view.E_EMailInputField.text))
            {
                ShowHelperWindow("请检查邮箱格式。");
                return;
            }

            if (!view.E_PasswordInputField.text.Equals(view.E_RePasswordInputField.text))
            {
                ShowHelperWindow("两个密码不相同。");
                return;
            }

            view.E_SubmitButton.interactable = false;
            int result = await LoginHelper.Regist(self.ZoneScene(), view.E_EMailInputField.text, view.E_PasswordInputField.text, view.E_VerificationInputField.text);
            view.E_SubmitButton.interactable = true;

            if (result != 0)
                DlgHelper.message = MessageHelper.GetMessage(result);
            else
                DlgHelper.message = "注册完成";
            uiCmp.ShowWindow(WindowID.WindowID_Helper);

            void ShowHelperWindow(string message)
            {
                DlgHelper.message = message;
                uiCmp.ShowWindow(WindowID.WindowID_Helper);
            }

            bool CheckInput(InputField inputField, string name)
            {
                if (string.IsNullOrEmpty(inputField.text))
                {
                    ShowHelperWindow($"未填写 {name}。");
                    return false;
                }
                return true;
            }
        }

        #endregion

        public static void ShowWindow(this DlgRegist self, Entity contextData = null)
        {
            var view = self.View;
            view.E_EMailInputField.text =
                view.E_VerificationInputField.text =
                view.E_PasswordInputField.text =
                view.E_RePasswordInputField.text = string.Empty;

            var scene = self.DomainScene();
            var uiCmp = scene.GetComponent<UIComponent>();

            switch (uiCmp.GetComponent<DlgRegistTypeComponent>().dlgType)
            {
                case DlgRegistType.RegistAccount:
                    view.E_TitleText.text = "注册";
                    view.E_SubmitTextText.text = "注     册";
                    view.E_InfoImage.gameObject.SetActive(true);
                    break;

                case DlgRegistType.ForgotPassword:
                default:
                    view.E_TitleText.text = "重置密码";
                    view.E_SubmitTextText.text = "提     交";
                    view.E_InfoImage.gameObject.SetActive(false);
                    break;
            }

            uiCmp.RemoveComponent<DlgRegistTypeComponent>();
        }
    }
}
