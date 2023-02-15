using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClass(typeof(TokenComponent))]
    public static class TokenComponentSystem
    {
        public static void AddOrSet(this TokenComponent self, long id, string token)
        {
            if (self.tokens.ContainsKey(id))
                self.tokens[id] = token;
            else
                self.tokens.Add(id, token);

            self.TimeOutRemoveToken(id, token).Coroutine(); //10分钟后检测该链接是否还存活
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <param name="id">目标ID</param>
        /// <param name="token">所得到的令牌</param>
        /// <returns>是否获取成功</returns>
        public static bool Get(this TokenComponent self, long id, out string token)
        {
            if (self.tokens.TryGetValue(id, out token))
                return true;

            return false;
        }

        public static void Remove(this TokenComponent self, long id)
        {
            self.tokens.Remove(id);
        }

        public static async ETTask TimeOutRemoveToken(this TokenComponent self, long id, string token)
        {
            await TimerComponent.Instance.WaitAsync(600000);    //等待十分钟

            if (self.Get(id, out string onlineToken) && onlineToken.Equals(token))
            {
                self.Remove(id);
            }

        }
    }

    [FriendClass(typeof(AccountCheckOutTimeComponent))]
    public static class AccountCheckOutTimeComponentSystem
    {
        public static void DeleteSession(this AccountCheckOutTimeComponent self, int reason)
        {
            Session session = self.GetParent<Session>();
            var ASC = session.DomainScene().GetComponent<AccountSessionsComponent>();
            ASC.Get(self.accountId, out long sessionInstanceId);
            if (sessionInstanceId == session.InstanceId)    //记录中的id与当前id是否一致，一致则说明无操作
            {
                ASC.Remove(sessionInstanceId);
            }

            session.Send(new A2C_Disconnect() { Error = reason });
            session?.Disconnect().Coroutine();
        }
    }

    public class AccountCheckOutTimeComponentAwakeSystem : AwakeSystem<AccountCheckOutTimeComponent, long>
    {
        public override void Awake(AccountCheckOutTimeComponent self, long accountId)
        {
            self.accountId = accountId;

            //启动定时器，计算无操作时间
            var timerCmp = TimerComponent.Instance;
            timerCmp.Remove(ref self.timer);

            self.timer = timerCmp.NewOnceTimer(TimeHelper.ServerNow() + 600000, TimerType.AccountCheckOutTime, self);
        }
    }

    public class AccountCheckOutTimeComponentDestroySystem : DestroySystem<AccountCheckOutTimeComponent>
    {
        public override void Destroy(AccountCheckOutTimeComponent self)
        {
            self.accountId = 0;
            TimerComponent.Instance.Remove(ref self.timer); //移除检测
        }
    }

    [Timer(TimerType.AccountCheckOutTime)]
    public class AccountSessionCheckOutTimer : ATimer<AccountCheckOutTimeComponent>
    {
        public override void Run(AccountCheckOutTimeComponent t)
        {
            try
            {
                t.DeleteSession(ErrorCode.ERR_TimeOut);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
        }
    }
}
