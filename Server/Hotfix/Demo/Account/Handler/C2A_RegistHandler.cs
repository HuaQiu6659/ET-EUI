using System;
using System.Text.RegularExpressions;

namespace ET
{
    [FriendClassAttribute(typeof(ET.Account))]
    public class C2A_RegistHandler : AMRpcHandler<C2A_Regist, A2C_Regist>
    {
        protected async override ETTask Run(Session session, C2A_Regist request, A2C_Regist response, Action reply)
        {
            if (!session.CheckScene(SceneType.Account))
                return;

            //避免同一连接多次登录请求
            if (session.GetComponent<SessionLockingComponent>() != null)
            {
                session.Reply(response, reply, ErrorCode.ERR_MultipleRequest, true);
                return;
            }

            //判定账号密码是否为空
            if (string.IsNullOrEmpty(request.Password) 
                || string.IsNullOrEmpty(request.EMail)
                || string.IsNullOrEmpty(request.Verification))
            {
                session.Reply(response, reply, ErrorCode.ERR_EmptyInput, true);
                return;
            }

            //限长度，防注入
            if (!Regex.IsMatch(request.Password, @"^[a-zA-Z0-9]+$")
                || !StringHelper.IsEmail(request.EMail))
            {
                session.Reply(response, reply, ErrorCode.ERR_IllegalInput, true);
                return;
            }

            //验证码
            if (!session.DomainScene().GetComponent<EMailComponent>().GetVerification(request.EMail, out string verification) || !verification.Equals(request.Verification))
            {
                session.Reply(response, reply, ErrorCode.ERR_WrongVerification, true);
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                //数据库检测账号密码
                var db = DBManagerComponent.Instance.GetZoneDB(session.DomainZone());
                var accountInfoList = await db.Query<Account>(d => d.email.Equals(request.EMail));
                Account account = null;
                if (accountInfoList?.Count > 0) //账号存在，替换密码
                {
                    account = accountInfoList[0];
                    account.password = request.Password;
                }
                else //注册
                {
                    account = session.AddChild<Account>();
                    account.email = request.EMail;
                    account.password = request.Password;
                    account.createTime = TimeHelper.ServerNow();
                    account.accountType = AccountType.General;
                }

                //根据Session的Zone区分不同区服的数据库
                await db.Save(account);
                session.Reply(response, reply, 0, true);
            }
        }
    }
}
