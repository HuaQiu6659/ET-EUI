namespace ET
{
    /// <summary>
    /// 挂载在连接上  避免服务器接收到同一连接的多次登录请求
    /// </summary>
    [ComponentOf(typeof(Session))]
    public class SessionLoginComponent : Entity, IAwake { }
}
