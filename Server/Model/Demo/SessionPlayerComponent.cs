namespace ET
{
	[ComponentOf(typeof(Session))]
	public class SessionPlayerComponent : Entity, IAwake, IDestroy
	{
		public long PlayerInstanceId;
		public long AccountId;
		public long SessionId;
	}
}