using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ComponentOf]
    public class DlgRegistTypeComponent : Entity, IAwake
    {
        public DlgRegistType dlgType = DlgRegistType.RegistAccount;
    }

    public enum DlgRegistType
    {
        RegistAccount,
        ForgotPassword
    }
}
