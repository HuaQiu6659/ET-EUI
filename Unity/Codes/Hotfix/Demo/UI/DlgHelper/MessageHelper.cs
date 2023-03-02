using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class MessageHelper
    {
        public static string GetMessage(int error)
        {
            switch (error)
            {
                case ErrorCode.ERR_EmptyInput:
                    return "输入为空。";

                case ErrorCode.ERR_IllegalInput:
                    return "非法字符。";

                case ErrorCode.ERR_Registed:
                    return "账号已被注册。";

                case ErrorCode.ERR_AccountEqualPassword:
                    return "账号与密码相同。";

                case ErrorCode.ERR_SendEMailFail:
                    return "获取验证码失败。";

                case ErrorCode.ERR_MultipleRequest:
                    return "操作过于频繁。";

                case ErrorCode.ERR_WrongVerification:
                    return "验证码错误。";

                default:
                    return string.Empty;
            }
        }
    }
}
