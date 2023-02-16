using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    [FriendClassAttribute(typeof(ET.SessionStateComponent))]
    public class C2G_LoginGameGateRequestHandler : AMRpcHandler<C2G_LoginGameGateRequest, G2C_LoginGameGateResponse>
    {
        protected override async ETTask Run(Session session, C2G_LoginGameGateRequest request, G2C_LoginGameGateResponse response, Action reply)
        {
            var currentScene = session.DomainScene().SceneType;
            if (currentScene != SceneType.Gate)
            {
                Log.Error($"请求Scene错误，目标Scene：Gate，当前Scene：{currentScene}");
                session.Dispose();
                return;
            }
            session.RemoveComponent<SessionAcceptTimeoutComponent>();   //基本等于登录成功，之后要保持长时间的连接

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            //判定Token
            var scene = session.DomainScene();
            var gateSessionKeyCmp = scene.GetComponent<GateSessionKeyComponent>();
            //Token不存在  或者  对不上
            if (!(gateSessionKeyCmp.Get(request.AccountId)?.Equals(request.Key)) ?? false)
            {
                response.Message = "Gate key 验证失败";
                RequestError(ErrorCode.ERR_WrongToken);
                return;
            }

            gateSessionKeyCmp.Remove(request.AccountId);
            long instanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, request.AccountId))
                {
                    if (instanceId != session.InstanceId)
                        return;

                    //通知登录中心服 登记本次登录的服务器zone
                    StartSceneConfig config = StartSceneConfigCategory.Instance.LoginCenterConfig;
                    L2G_AddLoginRecordResponse l2G_AddLogin = (L2G_AddLoginRecordResponse)await MessageHelper.CallActor(config.InstanceId, new G2L_AddLoginRecordRequest()
                    {
                        ServerId = session.DomainZone(),
                        AccountId = request.AccountId,
                    });

                    if (l2G_AddLogin.Error != ErrorCode.ERR_Success)
                    {
                        RequestError(l2G_AddLogin.Error);
                        return;
                    }

                    SessionStateComponent sessionStateCmp = session.GetComponent<SessionStateComponent>();
                    if (sessionStateCmp is null)
                        sessionStateCmp = session.AddComponent<SessionStateComponent>();
                    sessionStateCmp.state = SessionState.Normal;

                    var playerCmp = scene.GetComponent<PlayerComponent>();
                    //Player为客户端在Gate网关进程中的映射
                    Player player = playerCmp.Get(request.AccountId);

                    if (player == null)
                    {
                        player = playerCmp.AddChildWithId<Player, long, long>(request.RoleId, request.AccountId, request.RoleId);
                        player.PlayerState = PlayerState.Gate;
                        playerCmp.Add(player);
                        session.AddComponent<MailBoxComponent, MailboxType>(MailboxType.GateSession);
                    }
                    else
                    {
                        player.RemoveComponent<PlayerOfflineOutTimeComponent>();
                    }
                    var sessionPlayerCmp = session.AddComponent<SessionPlayerComponent>();
                    sessionPlayerCmp.PlayerInstanceId = player.InstanceId;
                    sessionPlayerCmp.AccountId = player.Account;
                    sessionPlayerCmp.SessionId = session.InstanceId;
                }
                reply();
            }


            void RequestError(int errorCode)
            {
                response.Error = errorCode;
                reply();
                session?.Disconnect().Coroutine();
            }
        }
    }
}
