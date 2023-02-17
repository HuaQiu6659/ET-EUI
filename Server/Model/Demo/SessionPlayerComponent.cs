namespace ET
{
	[ComponentOf(typeof(Session))]
	public class SessionPlayerComponent : Entity, IAwake, IDestroy
	{
		public long PlayerInstanceId;
		public long AccountId;

		//顶号操作标识
		public long SessionId;	//自写
		public bool isLoginAgain;	//字母哥写
	}
}