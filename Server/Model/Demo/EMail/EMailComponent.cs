using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class EMailComponent : Entity, IAwake
    {
        public SmtpClient smtp;
        public Dictionary<string, Verification> verifications; //(EMail : Verification)避免短时间内多次发送，记录验证码
    }

    public struct Verification
    {
        public string verification;
        public ETCancellationToken token;
    }
}
