/*
┌────────────────────────────┐
│　Description：菜单拓展
│　Author：花球i
└────────────────────────────┘
*/
using UnityEditor;
using System.IO;
using Object = UnityEngine.Object;

namespace Lin.Editor
{
    public static class PathHelper
    {
        /// <summary>
        /// 获取右键点击的文件夹位置
        /// </summary>
        /// <returns></returns>
        public static string GetSelectedPathOrFallback()
        {
            string path = "Assets";
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }
}