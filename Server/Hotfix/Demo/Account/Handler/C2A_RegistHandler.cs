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
            if (string.IsNullOrEmpty(request.Account) || string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.EMail))
            {
                session.Reply(response, reply, ErrorCode.ERR_EmptyInput, true);
                return;
            }

            //限长度，防注入
            if (!Regex.IsMatch(request.Account, @"^[a-zA-Z0-9]{6,17}$")
                || !Regex.IsMatch(request.Password, @"^[a-zA-Z0-9]+$")
                || !Regex.IsMatch(request.EMail, @"^[a-zA-Z0-9]+$"))
            {
                session.Reply(response, reply, ErrorCode.ERR_IllegalInput, true);
                return;
            }

            using (session.AddComponent<SessionLockingComponent>())
            {
                //数据库检测账号密码
                var db = DBManagerComponent.Instance;
                var accountInfoList = await db.GetZoneDB(session.DomainZone()).Query<Account>(d => d.account.Equals(request.Account));
                Account account = null;
                if (accountInfoList?.Count > 0) //账号存在
                {
                    session.Reply(response, reply, ErrorCode.ERR_Registed, true);
                    return;
                }
                else //注册
                {
                    //检测账号密码是否相同
                    if (request.Account.Equals(request.Password))
                    {
                        session.Reply(response, reply, ErrorCode.ERR_AccountEqualPassword, true);
                        return;
                    }

                    account = session.AddChild<Account>();
                    account.account = request.Account;
                    account.password = request.Password;
                    account.createTime = TimeHelper.ServerNow();
                    account.accountType = AccountType.General;

                    //根据Session的Zone区分不同区服的数据库
                    await db.GetZoneDB(session.DomainZone()).Save(account);

                    session.Reply(response, reply, 0, true);
                }
            }
        }
    }
}
