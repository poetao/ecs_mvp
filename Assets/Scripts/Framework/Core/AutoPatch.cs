using UnityEngine;

namespace Framework.Core
{
    public static class AutoPatch
    {
        public static string ResolveHotfix(string path)
        {
#if UNITY_EDITOR
            var prePath = System.IO.Path.GetFullPath($"{Application.streamingAssetsPath}/../../{Application.platform.ToString()}");
#else
            var prePath = Application.persistentDataPath;
#endif
            return prePath + "/" + path;
        }

        public static string ResolveHotfix4Web(string path)
        {
            var hotfixPath = ResolveHotfix(path);
#if UNITY_EDITOR || UNITY_IOS || UNITY_STANDALONE_OSX
            return $"file://{hotfixPath}";
#else
            return hotfixPath;
#endif
        }

    }
}