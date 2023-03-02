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

            EMailComponent mailComponent = session.DomainScene().GetComponent<EMailComponent>();

            //同一账号多次请求
            if(mailComponent.ContainEMail(request.EMail))
            {
                Finish(ErrorCode.ERR_MultipleRequest);
                return;
            }

            session.RemoveComponent<SessionAcceptTimeoutComponent>();
            using (session.AddComponent<SessionLockingComponent>())
            {
                using (await CoroutineLockComponent.Instance.Wait(CoroutineLockType.LoginOrRegiste, request.EMail.GetHashCode()))
                {
                    await mailComponent.SendVerification(request.EMail, response, EMailHelper.GetVerification());
                    Finish(response.Error);
                }
            }

            void Finish(int error = 0) => session.Reply(response, reply, error, true);
        }
    }
}
