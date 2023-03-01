using System;

namespace ET
{
    [FriendClassAttribute(typeof(ET.SessionPlayerComponent))]
    [FriendClassAttribute(typeof(ET.GateMapComponent))]
    [FriendClassAttribute(typeof(ET.SessionStateComponent))]
    public class C2G_EnterGameRequestHandler : AMRpcHandler<C2G_EnterGameRequest, G2C_EnterGameResponse>
    {
        protected override async ETTask Run(Session session, C2G_EnterGameRequest request, G2C_EnterGameResponse response, Action reply)
        {
            if (!session.CheckScene(SceneType.Gate))
                return;

            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                RequestError(ErrorCode.ERR_MultipleRequest);
                return;
            }

            var sessionPlayerCmp = session.GetComponent<SessionPlayerComponent>();
            if (sessionPlayerCmp is null)
            {
                RequestError(ErrorCode.ERR_SessionPlayerError);
                return;
            }

            var player = Game.EventSystem.Get(sessionPlayerCmp.PlayerInstanceId) as Player;
            if (player?.IsDisposed ?? true)
            {
                RequestError(ErrorCode.ERR_NonPlayer);
                return;
            }

            long sessionInstanceId = session.InstanceId;
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.GateLoginLock, player.Account))
                {
                    if (sessionInstanceId != session.InstanceId || player.IsDisposed)
                    {
                        RequestError(ErrorCode.ERR_SessionPlayerError);
                        return;
                    }
                }

                var sessionStateCmp = session.GetComponent<SessionStateComponent>();
                if (sessionStateCmp?.state == SessionState.Game)
                {
                    RequestError(ErrorCode.ERR_SessionStateError);
                    return;
                }

                if (player.PlayerState == PlayerState.Gaming)
                {
                    try
                    {
                        IActorResponse enterResponse = await MessageHelper.CallLocationActor(player.UnitId, new G2M_GetEnterGameStateRequest());
                        if (enterResponse.Error == ErrorCode.ERR_Success)   //逻辑服中存在对应对象
                        {
                            player.SessionInstanceId = session.InstanceId;
                            reply();
                            return;
                        }

                        Log.Error($"二次登录 {enterResponse.Error} | {enterResponse.Message}");
                        RequestError(ErrorCode.ERR_ReenterGameError, true);
                        await DisconnectHelper.KickPlayer(player, true);
                    }
                    catch (Exception e)
                    {
                        Log.Error($"二次登录失败  {e.Message}");
                        RequestError(ErrorCode.ERR_ReenterGameFail, true);
                        await DisconnectHelper.KickPlayer(player, true);
                    }
                    return;
                }

                try
                {
                    GateMapComponent gateMapComponent = player.AddComponent<GateMapComponent>();
                    gateMapComponent.Scene = await SceneFactory.Create(gateMapComponent, "GateMap", SceneType.Map);

                    Unit unit = UnitFactory.Create(gateMapComponent.Scene, player.Id, UnitType.Player);
                    unit.AddComponent<UnitGateComponent, long>(session.InstanceId);
                    long unitId = unit.InstanceId;

                    //将Unit转移到Map1中
                    StartSceneConfig config = StartSceneConfigCategory.Instance.GetBySceneName(session.DomainZone(), "Map1");
                    await TransferHelper.Transfer(unit, config.InstanceId, config.Name);

                    player.UnitId = unitId;
                    response.UnitId = unitId;

                    reply();

                    sessionStateCmp = session.GetComponent<SessionStateComponent>() ?? session.AddComponent<SessionStateComponent>();
                    sessionStateCmp.state = SessionState.Game;

                    player.PlayerState = PlayerState.Gaming;
                }
                catch (Exception e)
                {
                    Log.Error($"角色进入游戏逻辑服出现问题 账号ID:{player.Account} 角色ID:{player.Id} 异常信息:{e.Message}");
                    RequestError(ErrorCode.ERR_EnterGameError);
                    await DisconnectHelper.KickPlayer(player, true); 
                    session?.Disconnect().Coroutine();
                }
            }


            void RequestError(int errorCode, bool disconnect = false)
            {
                response.Error = errorCode;
                reply();

                if (disconnect)
                    session?.Disconnect().Coroutine();
            }
        }
    }
}
