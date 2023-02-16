namespace ET
{
    public static class DisconnectHelper
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

        public static async ETTask KickPlayer(Player player, bool isException = false)
        {
            if (player?.IsDisposed ?? true)
                return;

            long instanceId = player.InstanceId;
            using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, player.Account.GetHashCode()))
            {
                if (player.IsDisposed || instanceId != player.InstanceId)
                    return;

                if (!isException)
                    switch (player.PlayerState)
                    {
                        case PlayerState.Gaming:
                            //通知游戏逻辑服下线Unit角色，并将数据上传数据库
                            await MessageHelper.CallLocationActor(player.UnitId, new G2M_ExitGameRequest());

                            long loginCenterConfigSceneId = StartSceneConfigCategory.Instance.LoginCenterConfig.InstanceId;
                            await MessageHelper.CallActor(loginCenterConfigSceneId, new G2L_RemoveLoginRecordRequest() 
                            { 
                                AccountId = player.Account
                            });
                            break;

                        default:
                            break;
                    }

                player.PlayerState = PlayerState.Disconnect;
                player.DomainScene().GetComponent<PlayerComponent>()?.Remove(player.Account);
                player?.Dispose();
                await TimerComponent.Instance.WaitAsync(300);   //保证一些组件的异步方法完成
            }  
        }
    }
}
