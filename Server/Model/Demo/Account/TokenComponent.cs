using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 存储账号的登录令牌
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public class TokenComponent : Entity, IAwake
    {
        //令牌存储， ID:Token
        public readonly Dictionary<long, string> tokens = new Dictionary<long, string>();
    }
}
