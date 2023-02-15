using System;

namespace ET
{
    public static class SessionHelper
    {
        public static void RequestError(this Session session, int errorCode, IResponse response, Action reply)
        {
            response.Error = errorCode;
            reply();
            session?.Disconnect().Coroutine();
        }
    }
}