

namespace ET
{
	[FriendClass(typeof(SessionPlayerComponent))]
	public static class SessionPlayerComponentSystem
	{
		public class SessionPlayerComponentDestroySystem: DestroySystem<SessionPlayerComponent>
		{
			public override void Destroy(SessionPlayerComponent self)
			{
                //判断是为顶号登录还是主动断开

                //主动断开：DisconnectHelper.KickPlayer()
                var player = Game.EventSystem.Get(self.PlayerInstanceId) as Player;

				//若是顶号登录，Player的SessionInstanceId会发生变化

				//if (!self.isLoginAgain && player != null) //字母哥写
                if (player != null && player.SessionInstanceId == self.SessionId)
                    DisconnectHelper.KickPlayer(player).Coroutine();

				self.AccountId = 0;
				self.PlayerInstanceId = 0;
                self.isLoginAgain = false;
                //顶号登录：不执行DisconnectHelper.KickPlayer()，直接将Map中的Unit继承给新的登录
            }
		}

		public static Player GetMyPlayer(this SessionPlayerComponent self)
		{
			return self.Domain.GetComponent<PlayerComponent>().Get(self.AccountId);
		}
	}
}
