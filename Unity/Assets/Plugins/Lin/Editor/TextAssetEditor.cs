using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

namespace Lin.Editor
{
    [CustomEditor(typeof(TextAsset))]
    public class TextAssetEditor : UnityEditor.Editor
    {
        #region 创建TextAsset

        /// <summary>
        /// 创建文本类型资源时的命名响应
        /// </summary>
        class OnCreateText : EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                Object result = CreateScriptAssetFromTemplate(pathName);
                ProjectWindowUtil.ShowCreatedAsset(result);
            }

            internal static Object CreateScriptAssetFromTemplate(string pathName)
            {
                string fullPath = Path.GetFullPath(pathName);
                File.CreateText(fullPath).Close();
                AssetDatabase.ImportAsset(pathName);
                return AssetDatabase.LoadAssetAtPath(pathName, typeof(Object));
            }
        }

        [MenuItem("Assets/Create/TextAsset", false, 80)]
        static void CreateText()
        {
            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                CreateInstance<OnCreateText>(),
                PathHelper.GetSelectedPathOrFallback() + "/New Text.txt",
                null,
                default
                );
        }

        #endregion

        enum EditorState
        {
            Display,
            Input
        }

        EditorState state = EditorState.Display;
        string input;

        private void OnEnable()
        {
            state = EditorState.Display;
            string path = AssetDatabase.GetAssetPath(target);
            input = File.ReadAllText(path);
        }

        protected override void OnHeaderGUI()
        {
            base.OnHeaderGUI();

            if (state == EditorState.Display)
                GUILayout.Label(input);
            else
                input = GUILayout.TextArea(input);

            GUILayout.Space(10);

            GUILayout.BeginHorizontal();
            {
                EditorGUI.BeginDisabledGroup(state == EditorState.Input);
                if (GUILayout.Button("修改"))
                {
                    state = EditorState.Input;
                    string path = AssetDatabase.GetAssetPath(target);
                    input = File.ReadAllText(path);
                }
                EditorGUI.EndDisabledGroup();
            }
            {
                EditorGUI.BeginDisabledGroup(state == EditorState.Display);
                if (GUILayout.Button("保存"))
                {

                    string path = AssetDatabase.GetAssetPath(target);

                    state = EditorState.Display;
                    File.WriteAllText(path, input);
                }
                EditorGUI.EndDisabledGroup();
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
    }
}