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

        public static void Setup()
        {
            instance = new Path();     
        }

        private Path()
        {
            symboles = new Dictionary<string, string>();
        }

        public void Map(string symbole, string path)
        {
            symboles.Remove(symbole);
            if (string.IsNullOrEmpty(path)) return;

            symboles.Add(symbole, path);
        }

        public string Resolve(string path, TYPE type)
        {
            switch (type)
            {
                case TYPE.Prefab:       path = ResolvePrefab(path);      break;
                case TYPE.Scene:        path = ResolveScene(path);       break;
                case TYPE.AssetBundle:  path = ResolveAssetBundle(path); break;
                case TYPE.Presenter:    path = ResolvePresenter(path);   break;
                case TYPE.View:         path = ResolveView(path);        break;
                case TYPE.Storage:      path = ResolveStorage(path);     break;
                default: break;
            }

            return path;
        }

        private string ResolvePrefab(string path)
        {
            var finalPath = $"Assets/Prefabs/{path.Replace('.', '/')}.prefab";
            return finalPath.ToLower();
        }

        private string ResolveScene(string path)
        {
            return path;
        }

        private string ResolveAssetBundle(string path)
        {
            path = path.ToLower().Replace("assets/", "");
            return $"assets/assetBundles/{path}.ab";
        }

        private string ResolvePresenter(string path)
        {
            return "Presenters." + path.Replace("/", ".");
        }

        private string ResolveView(string path)
        {
            return "Views." + path.Replace("/", ".");
        }

        private string ResolveStorage(string path)
        {
            var prePath = Application.persistentDataPath;
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                prePath = Application.streamingAssetsPath;
            }

            return prePath + "/" + path.Replace(".", "/");
        }
    }
}
