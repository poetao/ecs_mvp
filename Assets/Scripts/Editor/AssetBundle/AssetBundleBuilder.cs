using System.IO;
using MVP.Framework.Core;
using UnityEditor;
using UnityEngine;

namespace MVP.Editors.AssetBundle
{
    using Path = System.IO.Path;
    public class AssetBundleBuilder
    {
        public static string outPath = $"Assets/{Framework.Core.Path.AssetBundleFolder}";

        [MenuItem("Tool/AssetBundle/Build")]
        public static void Build()
        {
            if (!Directory.Exists(outPath)) Directory.CreateDirectory(outPath);

            var manifest = BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
            if (manifest == null)
            {
                Log.Editor.E("Build AssetBundle Fail");
            }
        }

        [MenuItem("Tool/AssetBundle/Set Selector")]
        public static void SetSelectorAssetBundle()
        {
            AssetDatabase.RemoveUnusedAssetBundleNames();
            if (Selection.assetGUIDs.Length <= 0) return;

            var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
            CheckFileOrDirectory(path);
        }

        private static void CheckFileOrDirectory(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                SetFileAssetBundle(fileInfo);
            }
            else
            {
                SetFolderAssetBundle(fileInfo.ToString());
            }
        }

        public static void SetFolderAssetBundle(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFileSystemInfos())
            {
                CheckFileOrDirectory(file.ToString());
            }
        }

        public static void SetFileAssetBundle(FileInfo fileInfo)
        {
            if (fileInfo.Extension == ".meta") return;
            if (fileInfo.Name.EndsWith("~")) return;

            var path = fileInfo.FullName
                .Replace(Path.DirectorySeparatorChar, '/')
                .Replace(Application.dataPath, "Assets");
            var importer = AssetImporter.GetAtPath(path);
            if (importer == null)
            {
                Log.Editor.W($"获取 {path} importer 为空");
                return;
            }

            importer.assetBundleVariant = "ab";
            importer.assetBundleName = path.Replace("Assets/", "").Replace('/', '.');
            Log.Editor.I($"设置 {path} AssetBundle 成功");
        }
    }
}