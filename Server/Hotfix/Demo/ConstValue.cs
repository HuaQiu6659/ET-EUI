using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class ConstValue
    {
        // -------------- 验证码邮箱 --------------
        public const MailType EMail_Type = MailType.NetEase;
        public const string EMail_Tencent_Sender = "yao_senlin@foxmail.com";
        public const string EMail_Tencent_Authorization = "elbayoxymiwfdcgj";
        public const int EMail_Tencent_Port = 587;
        public const string EMail_Tencent_Host = "smtp.qq.com";

        public const string EMail_NetEase_Sender = "mumugame975@163.com";
        public const string EMail_NetEase_Authorization = "XGXQFPDKPMXZPANC";
        public const int EMail_NetEase_Port = 25;
        public const string EMail_NetEase_Host = "smtp.163.com";
        //腾讯：yao_senlin@foxmail.com     elbayoxymiwfdcgj
        //网易：mumugame975@163.com        XGXQFPDKPMXZPANC
    }
}
