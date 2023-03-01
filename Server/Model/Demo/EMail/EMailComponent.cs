using System.Net.Mail;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class EMailComponent : Entity, IAwake
    {
        public SmtpClient smtp;
    }
}
