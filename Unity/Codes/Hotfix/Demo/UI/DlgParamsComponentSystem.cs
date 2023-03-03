using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [FriendClassAttribute(typeof(ET.DlgParamsComponent))]
    public static class DlgParamsComponentSystem
    {
        public static void SetValue<T>(this DlgParamsComponent self, string key, T value)
        {
            var type = typeof(T);
            Dictionary<string, T> dict = null;
            if (self.objects.ContainsKey(type))
            {
                dict = self.objects[typeof(T)] as Dictionary<string, T>;
            }
            else
            {
                dict = new Dictionary<string, T>();
                self.objects.Add(type, dict);
                goto AddValue;
            }

            if (dict.ContainsKey(key))
            {
                dict[key] = value;
                return;
            }

        AddValue:
            dict.Add(key, value);
        }

        public static T GetValue<T>(this DlgParamsComponent self, string key)
        {
            Type type = typeof(T);
            if (!self.objects.TryGetValue(type, out IDictionary value))
                return default;

            var dict = value as Dictionary<string, T>;
            dict.TryGetValue(key, out T result);
            return result;
        }

        public class DlgParamsComponentAwakeSystem : AwakeSystem<DlgParamsComponent>
        {
            public override void Awake(DlgParamsComponent self)
            {
                self.objects = new Dictionary<Type, System.Collections.IDictionary>();
            }
        }
    }
}
