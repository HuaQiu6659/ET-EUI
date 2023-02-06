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
    }
}
