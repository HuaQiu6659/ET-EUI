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
            mailMessage.From = new MailAddress(ConstValue.EMail_Type == MailType.NetEase ? ConstValue.EMail_NetEase_Sender : ConstValue.EMail_Tencent_Sender);
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

        public static SmtpClient GetSmtpClient(MailType type)
        {
            SmtpClient client = new SmtpClient();

            switch (type)
            {
                case MailType.Tencent:
                    client.Port = ConstValue.EMail_Tencent_Port;
                    client.Host = ConstValue.EMail_Tencent_Host;
                    client.Credentials = new NetworkCredential(ConstValue.EMail_Tencent_Sender, ConstValue.EMail_Tencent_Authorization);
                    break;

                case MailType.NetEase:
                default:
                    client.Port = ConstValue.EMail_NetEase_Port;
                    client.Host = ConstValue.EMail_NetEase_Host;
                    client.Credentials = new NetworkCredential(ConstValue.EMail_NetEase_Sender, ConstValue.EMail_NetEase_Authorization);
                    break;
            }
            //使用安全加密连接（是否启用SSL）
            client.EnableSsl = true;

            //设置超时时间
            client.Timeout = 1000;
            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            return client;
        }
    }
}
