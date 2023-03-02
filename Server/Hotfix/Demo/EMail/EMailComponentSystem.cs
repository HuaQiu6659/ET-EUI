using System;
using System.Net.Mail;

namespace ET
{
    [FriendClass(typeof(ET.EMailComponent))]
    public static class EMailComponentSystem
    {
        public static async ETTask SendVerification(this EMailComponent self, string receiver, A2C_Verification response, string verification)
        {
            MailMessage mailMessage = EMailHelper.GetMailMessage(receiver);

            //抄送人邮箱地址。
            //邮件标题。
            mailMessage.Subject = "【木木游戏】EMail地址验证";
            //邮件内容。
            mailMessage.Body = $"EMail地址验证\r\n您正在请求 注册新账户 的操作验证码, 您的验证码是:\r\n{verification}\r\n请不要向其他人提供此验证码, 这可能使您的账户遭受攻击\r\n这是系统自动发送的邮件，请不要回复此邮件\r\n如果该验证码不是您本人请求的, 请忽略此邮件";

            try
            {
                //发送
                await self.smtp.SendMailAsync(mailMessage);
                Log.Debug($"Success to send verification to {receiver}.");
                self.RecordEMail(receiver, verification);
            }
            catch (Exception e)//发送异常
            {
                response.Error = ErrorCode.ERR_SendEMailFail;
                response.Message = e.Message;
                Log.Error($"Fail to send verification to {receiver}.\n{e.Message}");
            }
        }

        static async void RecordEMail(this EMailComponent self, string email, string verification)
        {
            Verification container;
            if (self.verifications.ContainsKey(email))
            {
                container = self.verifications[email];
                container.token.Cancel();
                self.verifications.Remove(email);
            }
            container = new Verification() { token = new ETCancellationToken(), verification = verification };
            self.verifications.Add(email, container);
            await TimerComponent.Instance.WaitAsync(300000, container.token); //等待5分钟
            self.verifications.Remove(email);
        }

        public static bool ContainEMail(this EMailComponent self, string email) => self.verifications.ContainsKey(email);

        public static bool GetVerification(this EMailComponent self, string email, out string verification)
        {
            if (self.verifications.TryGetValue(email, out Verification container))
            {
                verification = container.verification;
                return true;
            }
            else
            {
                verification = String.Empty;
                return false;
            }
        }

        public class MailComponentAwakeSystem : AwakeSystem<EMailComponent>
        {
            public override void Awake(EMailComponent self)
            {
                self.smtp = EMailHelper.GetSmtpClient(ConstValue.EMail_Type);
                self.verifications = new System.Collections.Generic.Dictionary<string, Verification>();
            }
        }

    }
}
