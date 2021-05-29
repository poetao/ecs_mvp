using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    using TYPE = Resource.TYPE;

    public class Path
    {
        [Serializable]
        public enum ASSEMBLY_TYPE
        {
            GAME, FRAMEWORK
        }

        public static Path instance { get; private set; }
        public static string AssetBundleFolder = "AssetBundles";

        private readonly Dictionary<string, string> symboles;
        private Dictionary<ASSEMBLY_TYPE, string> assemblies;

        public static void Setup()
        {
            instance = new Path();     
        }

        private Path()
        {
            symboles   = new Dictionary<string, string>();
            assemblies = new Dictionary<ASSEMBLY_TYPE, string>
            {
                {ASSEMBLY_TYPE.GAME, "Game"}, {ASSEMBLY_TYPE.FRAMEWORK, "Framework"}
            };
        }

        public string GetAssemblyPath(ASSEMBLY_TYPE assembly)
        {
            if (!assemblies.ContainsKey(assembly)) return "";

            return assemblies[assembly];
        }

        public void Map(string symbole, TYPE type, string path, ASSEMBLY_TYPE assembly)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(symbole))
            {
                return;
            }

            var key = $"{assembly}_{type}_{symbole}";
            if (symboles.ContainsKey(key)) return;

            symboles.Add(key, path);
        }

        public string Resolve(string symbole, TYPE type, ASSEMBLY_TYPE assembly = ASSEMBLY_TYPE.GAME)
        {
            var key = $"{assembly}_{type}_{symbole}";
            if (symboles.TryGetValue(key, out var path))
            {
                return path;
            }

            switch (type)
            {
                case TYPE.Prefab:       path = ResolvePrefab(symbole);                break;
                case TYPE.Scene:        path = ResolveScene(symbole);                 break;
                case TYPE.AssetBundle:  path = ResolveAssetBundle(symbole);           break;
                case TYPE.Presenter:    path = ResolvePresenter(symbole, assembly);   break;
                case TYPE.View:         path = ResolveView(symbole, assembly);        break;
                case TYPE.Component:    path = ResolveComponent(symbole, assembly);   break;
                case TYPE.Storage:      path = ResolveStorage(symbole);               break;
            }
            Map(symbole, type, path, assembly);

            return path;
        }

        private string ResolvePrefab(string path)
        {
            return $"Assets/Prefabs/{path.Replace('.', '/')}.prefab".ToLower();
        }

        private string ResolveScene(string path)
        {
            return path;
        }

        private string ResolveAssetBundle(string path)
        {
            var finalPath = path.ToLower().Replace("assets/", "");
            finalPath = $"assets/assetBundles/{finalPath}.ab";
            return finalPath;
        }

        private string ResolvePresenter(string path, ASSEMBLY_TYPE assembly)
        {
            return GetAssemblyPath(assembly) + ".Presenters." + path.Replace("/", ".");
        }

        private string ResolveView(string path, ASSEMBLY_TYPE assembly)
        {
            return GetAssemblyPath(assembly) + ".Views." + path.Replace("/", ".");
        }

        private string ResolveComponent(string path, ASSEMBLY_TYPE assembly)
        {
            return GetAssemblyPath(assembly) + ".Components." + path.Replace("/", ".");
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
