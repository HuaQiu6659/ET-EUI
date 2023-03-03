using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public class Account : Entity, IAwake
    {
        public string email;
        public string password;
        public AccountType accountType;

        public long createTime;
        public long lastLoginTime;
    }

    public enum AccountType
    {
        //一般用户
        General,

        //黑名单
        BlackList
    }
}
