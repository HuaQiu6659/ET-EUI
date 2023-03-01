using System;

namespace ET
{
    public class C2A_VerificationHandler : AMRpcHandler<C2A_Verification, A2C_Verification>
    {
        protected async override ETTask Run(Session session, C2A_Verification request, A2C_Verification response, Action reply)
        {
            if (!session.CheckScene(SceneType.Account))
                return;

            //避免同一连接多次登录请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                Finish(ErrorCode.ERR_MultipleRequest);
                return;
            }

            if (!StringHelper.IsEmail(request.EMail))
            {
                Finish(ErrorCode.ERR_IllegalInput);
                return;
            }

            //经常会超过5秒，之后直接手动释放
            session.RemoveComponent<SessionAcceptTimeoutComponent>();

            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginOrRegiste, request.EMail.GetHashCode()))
                {
                    response.Verification = EMailHelper.GetVerification();
                    await session.DomainScene().GetComponent<EMailComponent>().SendVerification(request.EMail, response);
                    Finish(response.Error);
                }
            }

            void Finish(int error = 0) => session.Reply(response, reply, error, true);
        }
    }
}
