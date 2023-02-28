namespace ET
{
    public static partial class ErrorCode
    {
        public const int ERR_Success = 0;

        // 1-11004 是SocketError请看SocketError定义
        //-----------------------------------
        // 100000-109999是Core层的错误

        // 110000以下的错误请看ErrorCore.cs

        // 这里配置逻辑层的错误码
        // 110000 - 200000是抛异常的错误
        // 200001以上不抛异常

        //登录部分
        public const int ERR_BlackList              = 200001;   //账号封禁
        public const int ERR_WrongPassword          = 200002;   //密码错误
        public const int ERR_LoginElsewhere         = 200003;   //在其他地方登录
        public const int ERR_AccountEqualPassword   = 200004;   //账号密码相同
        public const int ERR_RoleExisted            = 200005;   //同区同名角色已存在
        public const int ERR_RoleUnexisted          = 200006;   //删除角色时角色不存在
        public const int ERR_SessionPlayerError     = 200007;
        public const int ERR_SessionStateError      = 200008;
        public const int ERR_EnterGameError         = 200009;
        public const int ERR_ReenterGameError       = 200010;
        public const int ERR_ReenterGameFail        = 200011;
        public const int ERR_Registed               = 200012; //账号已被注册

        //通用部分
        public const int ERR_WrongScene             = 300000;
        public const int ERR_NetworkError           = 300001;   //网络错误
        public const int ERR_EmptyInput             = 300002;
        public const int ERR_IllegalInput           = 300003;   //非法字符串
        public const int ERR_MultipleRequest        = 300004;   //多次请求
        public const int ERR_TimeOut                = 300005;   //时间过长（无操作）
        public const int ERR_WrongToken             = 300006;   //Token不存在或者对不上
        public const int ERR_NonPlayer              = 300007;

    }
}