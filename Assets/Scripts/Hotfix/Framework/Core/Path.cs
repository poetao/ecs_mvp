using System;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.Core
{
    using RES_TYPE = Resource.TYPE;

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
        private Dictionary<ASSEMBLY_TYPE, Tuple<string, string>> assemblies;

        public static void Setup()
        {
            instance = new Path();
        }

        private Path()
        {
            symboles   = new Dictionary<string, string>();
            assemblies = new Dictionary<ASSEMBLY_TYPE, Tuple<string, string>>
            {
                {ASSEMBLY_TYPE.GAME, Tuple.Create("Hotfix", "Game")},
                {ASSEMBLY_TYPE.FRAMEWORK, Tuple.Create("Hotfix", "Framework")}
            };
        }

        public string GetAssemblyName(ASSEMBLY_TYPE assembly)
        {
            if (!assemblies.ContainsKey(assembly)) return "";

            return assemblies[assembly].Item1;
        }

        public string GetAssemblyPath(ASSEMBLY_TYPE assembly)
        {
            if (!assemblies.ContainsKey(assembly)) return "";

            return assemblies[assembly].Item2;
        }

        public void Map(string symbole, RES_TYPE type, string path, ASSEMBLY_TYPE assembly)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(symbole))
            {
                return;
            }

            var key = $"{assembly}_{type}_{symbole}";
            if (symboles.ContainsKey(key)) return;

            symboles.Add(key, path);
        }

        public string Resolve(string symbole, RES_TYPE type, ASSEMBLY_TYPE assembly = ASSEMBLY_TYPE.GAME)
        {
            var key = $"{assembly}_{type}_{symbole}";
            if (symboles.TryGetValue(key, out var path))
            {
                return path;
            }

            switch (type)
            {
                case RES_TYPE.Prefab:       path = ResolvePrefab(symbole);                break;
                case RES_TYPE.Scene:        path = ResolveScene(symbole);                 break;
                case RES_TYPE.AssetBundle:  path = ResolveAssetBundle(symbole);           break;
                case RES_TYPE.Presenter:    path = ResolvePresenter(symbole, assembly);   break;
                case RES_TYPE.View:         path = ResolveView(symbole, assembly);        break;
                case RES_TYPE.Component:    path = ResolveComponent(symbole, assembly);   break;
                case RES_TYPE.Storage:      path = ResolveStorage(symbole);               break;
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
