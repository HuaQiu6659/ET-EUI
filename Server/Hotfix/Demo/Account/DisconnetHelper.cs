namespace ET
{
    public static class DisconnetHelper
    {
        public static async ETTask Disconnect(this Session session)
        {
            if (session?.IsDisposed ?? true)
                return;

            long instanceId = session.InstanceId;

            //若1秒内ID发生变化，则说明该Session可能被提前释放（释放后InstanceID归0）
            await TimerComponent.Instance.WaitAsync(1000);
            if (session.InstanceId != instanceId)
                return;

            session.Dispose();
        }

        public static async ETTask KickPlayer(Player target)
        {
            if (target?.IsDisposed ?? true)
                return;

            long instanceId = target.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, target.Account.GetHashCode()))
            {
                if (target.IsDisposed || instanceId != target.InstanceId)
                    return;

                switch (target.PlayerState)
                {
                    case PlayerState.Gaming:
                        //TODO:通知客户端下线，上传数据
                        break;

                    default:
                        break;
                }

                target.PlayerState = PlayerState.Disconnect;
                target.DomainScene().GetComponent<PlayerComponent>()?.Remove(target.Account);
                target?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);   //保证一些组件的异步方法完成
            }  
        }
    }
}
