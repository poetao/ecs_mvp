using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using MVP.Framework.Core;
using MVP.Framework.Compoents.Windows;

namespace MVP.Framework.Windows
{
    public struct ContainerInfo
    {
        public static string path                = "Framework/Window/Container";
        public static Type component             = typeof(Container);
        public static Resources.AssetRef prefab  = null;
    }
    
    public class Factory
    {
        public static async Task<Factory> Create()
        {
            ContainerInfo.prefab = await Resource.instance.LoadAsync(ContainerInfo.path);
            return new Factory();            
        }

        private Factory() {}

        public async Task<LinkData> Load(string path, ILinkDataManager manager, params object[] args)
        {
            var prefab = await Resource.instance.LoadAsync(path);
            if (prefab == null)
            {
                Log.Window.Exception(new Exception($"Factory Load {path} prefab error!"));
            }

            var data = Utils.Instantitate(prefab, path, manager, args);
            prefab.Release();
            return data;
        }

        public async Task<LinkData> Instantitate(ILinkDataManager manager, string path, params object[] args)
        {
            var container   = Utils.Instantitate(ContainerInfo.prefab, ContainerInfo.path, null) as LinkData;
            var components  = Utils.GetComponent(container.node, ContainerInfo.component);
            var component   = components[0] as Container;
            var data        = await Load(path, manager, args);

            TryAddToCanvas(container.node);
            component.Bind(manager, data);
            return component.GetTarget();
        }

        private void TryAddToCanvas(GameObject node)
        {
            var gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var gameObject in gameObjects)
            {
                var canvas = gameObject.GetComponent<Canvas>();
                if (canvas == null) continue;

                node.transform.SetParent(canvas.transform, false);
                return;
            }
        }
    }
}
