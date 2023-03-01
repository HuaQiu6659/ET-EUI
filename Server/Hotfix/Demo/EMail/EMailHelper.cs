using System;
using System.Net.Mail;
using System.Net;

namespace ET
{
    public static class EMailHelper
    {
        //https://www.cnblogs.com/yinmu/p/13826065.html
        public static MailMessage GetMailMessage(string receiver)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(ConstValue.EMail_Sender);
            mailMessage.To.Add(new MailAddress(receiver));
            return mailMessage;
        }

        public static string GetVerification()
        {
            Random random = new Random();
            return random.Next(1000, 10000).ToString();
        }

        public static string GetHost(MailType type)
        {
            switch (type)
            {
                case MailType.Tencent:
                    return "smtp.qq.com";

                case MailType.NetEase:
                default:
                    return "smtp.163.com";
            }
        }

        public static SmtpClient GetSmtpClient()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = GetHost(ConstValue.EMail_Type);
            //使用安全加密连接（是否启用SSL）
            client.EnableSsl = true;

            //设置超时时间
            //client.Timeout = 10000;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
            client.Credentials = new NetworkCredential(ConstValue.EMail_Sender, ConstValue.EMail_Authorization);

            return client;
        }
    }

    [FriendClass(typeof(ET.EMailComponent))]
    public static class EMailComponentSystem
    {
        public static async ETTask SendVerification(this EMailComponent self, string receiver, A2C_Verification response)
        {
            MailMessage mailMessage = EMailHelper.GetMailMessage(receiver);

            //抄送人邮箱地址。
            //邮件标题。
            mailMessage.Subject = "【木木游戏】EMail地址验证";
            //邮件内容。
            mailMessage.Body = $"EMail地址验证\r\n您正在请求 注册新账户 的操作验证码, 您的验证码是:\r\n{response.Verification}\r\n请不要向其他人提供此验证码, 这可能使您的账户遭受攻击\r\n这是系统自动发送的邮件，请不要回复此邮件\r\n如果该验证码不是您本人请求的, 请忽略此邮件";

            try
            {
                var client = EMailHelper.GetSmtpClient();
                //发送
                await client.SendMailAsync(mailMessage);
                Log.Debug($"Success to send verification to {receiver}.");
            }
            catch (Exception e)//发送异常
            {
                response.Error = ErrorCode.ERR_SendEMailFail;
                response.Message = e.Message;
                Log.Error($"Fail to send verification to {receiver}.\n{e.Message}");
            }
        }

        public class MailComponentAwakeSystem : AwakeSystem<EMailComponent>
        {
            public override void Awake(EMailComponent self)
            {
                var stmp = self.smtp = new SmtpClient();
                stmp.Port = 587;
                stmp.Host = EMailHelper.GetHost(ConstValue.EMail_Type);

                //使用安全加密连接（是否启用SSL）
                stmp.EnableSsl = true;

                //设置超时时间
                //client.Timeout = 10000;
                //不和请求一块发送。
                stmp.UseDefaultCredentials = false;

                //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                stmp.Credentials = new NetworkCredential(ConstValue.EMail_Sender, ConstValue.EMail_Authorization);
            }
        }

    }
}
