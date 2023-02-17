using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class UnitCacheHelper
    {
        /// <summary>
        /// 添加或更新缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static async ETTask AddOrUpdateUnitCache<T>(this T self) where T : Entity, IUnitCache
        {
            Other2UnitCache_AddOrUpdateUnitRequest request = new Other2UnitCache_AddOrUpdateUnitRequest() { UnitId = self.Id };
            request.EnitityTypes.Add(self.GetType().FullName);
            request.EntityBytes.Add(MongoHelper.ToBson(self));
            await MessageHelper.CallActor(StartSceneConfigCategory.Instance.GetUnitCacheConfig(self.Id).InstanceId, request);
        }

        /// <summary>
        /// 获取玩家组建缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="unitId">组件ID</param>
        /// <returns></returns>
        public static async ETTask<T> GetUnitComponent<T>(long unitId) where T : Entity, IUnitCache
        {
            Other2UnitCache_GetUnitRequest request = new Other2UnitCache_GetUnitRequest() { UnitId = unitId };
            request.ComponentNameList.Add(typeof(T).Name);
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            UnitCache2Other_GetUnitResponse response = (UnitCache2Other_GetUnitResponse)await MessageHelper.CallActor(instanceId, request);
            if (response.Error == ErrorCode.ERR_Success && response.EntityList.Count > 0)
                return response.EntityList[0] as T;

            return null;
        }

        /// <summary>
        /// 删除实体缓存
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static async ETTask DeleteUnitCache(long unitId)
        {
            Other2UnitCache_DeleteUnitRequest request = new Other2UnitCache_DeleteUnitRequest() { UnitId = unitId };
            long instanceId = StartSceneConfigCategory.Instance.GetUnitCacheConfig(unitId).InstanceId;
            await MessageHelper.CallActor(instanceId, request);
        }
    }
}
