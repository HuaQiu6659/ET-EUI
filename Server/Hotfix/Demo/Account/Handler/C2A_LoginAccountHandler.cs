using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ET
{
    //登录请求处理
    [FriendClass(typeof(Account))]
    public class C2A_LoginAccountHandler : AMRpcHandler<C2A_LoginAccount, A2C_LoginAccount>
    {
        protected override async ETTask Run(Session session, C2A_LoginAccount request, A2C_LoginAccount response, Action reply)
        {
            if (!session.CheckScene(SceneType.Account))
                return;

            //带有该组件时新创建的连接只会维持5秒，当前视为通过连接验证，因此去除该限制
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            //避免同一连接多次登录请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                session.Reply(response, reply, ErrorCode.ERR_MultipleRequest, true);
                return;
            }

            //判定账号密码是否为空
            if (string.IsNullOrEmpty(request.EMail) || string.IsNullOrEmpty(request.Password))
            {
                session.Reply(response, reply, ErrorCode.ERR_EmptyInput, true);
                return;
            }

            //限长度，防注入
            if (!StringHelper.IsEmail(request.EMail)
                || StringHelper.HasSymbol(request.Password))
            {
                session.Reply(response, reply, ErrorCode.ERR_IllegalInput, true);
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                //协程锁 保证同一账号不会再多处同时被注册
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginOrRegiste, request.EMail.GetHashCode()))
                {
                    //数据库检测账号密码
                    var db = DBManagerComponent.Instance;
                    var accountInfoList = await db.GetZoneDB(session.DomainZone()).Query<Account>(d => d.email.Equals(request.EMail));
                    Account account = null;
                    if (accountInfoList?.Count > 0) //账号存在
                    {
                        account = accountInfoList.First();
                        session.AddChild(account);

                        //判断账号密码正确性
                        if (!account.password.Equals(request.Password))
                        {
                            session.Reply(response, reply, ErrorCode.ERR_WrongPassword, true);
                            account?.Dispose();
                            return;
                        }

                        if (account.accountType == AccountType.BlackList)
                        {
                            session.Reply(response, reply, ErrorCode.ERR_BlackList, true);
                            account?.Dispose();
                            return;
                        }
                    }
                    else //不存在
                    {
                        session.Reply(response, reply, ErrorCode.ERR_Unregist, true);
                        return;
                    }

                    //向登录中心服发送请求
                    StartSceneConfig startSceneConfig = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "LoginCenter");
                    long loginCenterInstanceId = startSceneConfig.InstanceId;
                    L2A_LoginAccountResponse l2A_LoginAccountResponse = (L2A_LoginAccountResponse)await ActorMessageSenderComponent.Instance.Call(loginCenterInstanceId, new A2L_LoginAccountRequest() { AccountId = account.Id });
                    if (l2A_LoginAccountResponse.Error != ErrorCode.ERR_Success)
                    {
                        session.Reply(response, reply, l2A_LoginAccountResponse.Error, true);
                        return;
                    }

                    AccountSessionsComponent ASC = session.DomainScene().GetComponent<AccountSessionsComponent>();
                    //判断账号是否在线
                    if (ASC.Get(account.Id, out long sessionInstanceId))
                    {
                        //账号在线，将其踢下线
                        Session otherSession = Game.EventSystem.Get(sessionInstanceId) as Session;
                        if (otherSession != null)
                        {
                            otherSession.Send(new A2C_Disconnect() { Error = ErrorCode.ERR_LoginElsewhere });
                            otherSession?.Disconnect().Coroutine();
                        }
                    }
                    ASC.AddOrSet(account.Id, session.InstanceId);
                    //登录后10分钟后若无操作将被踢下线（避免客户端断联后服务端不知道）
                    session.AddComponent<AccountCheckOutTimeComponent, long>(account.Id);

                    //生成连接令牌
                    string token = TimeHelper.ServerNow().ToString() + RandomHelper.RandInt32().ToString();
                    session.DomainScene().GetComponent<TokensComponent>().AddOrSet(account.Id, token);

                    //返回给客户端
                    response.AccountId = account.Id;
                    response.Token = token;
                    reply();
                    account?.Dispose();
                }
            }
        }
    }
}
