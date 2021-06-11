using UnityEditor;
using UnityEngine;

namespace ILRuntime.Editor
{
    [System.Reflection.Obfuscation(Exclude = true)]
    public class ILRuntimeMenu
    {
       [MenuItem("ILRuntime/安装VS调试插件")]
        static void InstallDebugger()
        {   
            EditorUtility.OpenWithDefaultApp("../Plugins/ILRuntime Debugger/ILRuntimeDebuggerLauncher.vsix");
        }

        [MenuItem("ILRuntime/打开ILRuntime中文文档")]
        static void OpenDocumentation()
        {
            Application.OpenURL("https://ourpalm.github.io/ILRuntime/");
        }

        [MenuItem("ILRuntime/打开ILRuntime Github项目")]
        static void OpenGithub()
        {
            Application.OpenURL("https://github.com/Ourpalm/ILRuntime");
        }
    }
}
