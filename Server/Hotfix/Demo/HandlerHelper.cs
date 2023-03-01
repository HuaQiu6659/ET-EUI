using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class HandlerHelper
    {
        public static bool CheckScene(this Session session, SceneType targetScene)
        {
            var currentScene = session.DomainScene().SceneType;
            if (currentScene != targetScene)
            {
                Log.Error($"请求Scene错误，目标Scene：{targetScene}，当前Scene：{currentScene}");
                session.Dispose();
                return false;
            }
            return true;
        }

        public static bool CheckScene(this Scene scene, SceneType targetScene, IResponse response, Action reply)
        {
            if (scene.SceneType != targetScene)
            {
                Log.Error($"请求Scene错误，目标Scene：{targetScene}，当前Scene：{scene.SceneType}");
                response.Error = ErrorCode.ERR_WrongScene;
                reply();
                return false;
            }
            return true;
        }

        public static void Reply(this Session session, IResponse response, Action reply, int errorCode = ErrorCode.ERR_Success, bool disconnect = false)
        {
            response.Error = errorCode;
            reply();
            if (disconnect)
                session?.Disconnect().Coroutine();
        }
    }
}
