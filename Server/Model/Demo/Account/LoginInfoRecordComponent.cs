﻿using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class LoginInfoRecordComponent : Entity, IAwake, IDestroy
    {
        public Dictionary<long, int> accountLoginInfos = new Dictionary<long, int>();
    }
}
