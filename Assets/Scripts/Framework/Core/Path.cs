using System.Collections.Generic;
using UnityEngine;

namespace MVP.Framework.Core
{
    using TYPE = Resource.TYPE;

    public class Path
    {
        public static Path instance { get; private set; }
        public static string AssetBundleFolder = "AssetBundles";

        private Dictionary<string, string> symboles;
        private Dictionary<string, string> assemblies;

        public static void Setup()
        {
            instance = new Path();     
        }

        private Path()
        {
            symboles   = new Dictionary<string, string>();
            assemblies = new Dictionary<string, string>
            {
                {"Game", ""}, {"Framework", "MVP.Framework."}
            };
        }

        public void Map(string symbole, string path)
        {
            symboles.Remove(symbole);
            if (string.IsNullOrEmpty(path)) return;

            symboles.Add(symbole, path);
        }

        public string Resolve(string path, TYPE type, string assembly = null)
        {
            switch (type)
            {
                case TYPE.Prefab:       path = ResolvePrefab(path, assembly);      break;
                case TYPE.Scene:        path = ResolveScene(path, assembly);       break;
                case TYPE.AssetBundle:  path = ResolveAssetBundle(path, assembly); break;
                case TYPE.Presenter:    path = ResolvePresenter(path, assembly);   break;
                case TYPE.View:         path = ResolveView(path, assembly);        break;
                case TYPE.Component:    path = ResolveComponent(path, assembly);   break;
                case TYPE.Storage:      path = ResolveStorage(path, assembly);     break;
                default: break;
            }

            return path;
        }

        private string ResolvePrefab(string path, string assembly)
        {
            var finalPath = $"Assets/Prefabs/{path.Replace('.', '/')}.prefab";
            return finalPath.ToLower();
        }

        private string ResolveScene(string path, string assembly)
        {
            return path;
        }

        private string ResolveAssetBundle(string path, string assembly)
        {
            path = path.ToLower().Replace("assets/", "");
            return $"assets/assetBundles/{path}.ab";
        }

        private string ResolvePresenter(string path, string assembly)
        {
            return "Presenters." + path.Replace("/", ".");
        }

        private string ResolveView(string path, string assembly)
        {
            return "Views." + path.Replace("/", ".");
        }

        private string ResolveComponent(string path, string assembly)
        {
            return GetAssemblyPath(assembly) + "Components." + path.Replace("/", ".");
        }

        private string ResolveStorage(string path, string assembly)
        {
            var prePath = Application.persistentDataPath;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                prePath = Application.streamingAssetsPath;
            }

            return prePath + "/" + path.Replace(".", "/");
        }

        private string GetAssemblyPath(string assembly)
        {
            if (string.IsNullOrEmpty(assembly)) return "";
            if (!assemblies.ContainsKey(assembly)) return "";

            return assemblies[assembly];
        }
    }
}
