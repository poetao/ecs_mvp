using UnityEngine;
using UniRx;
using MVP.Framework.Bootstraps;

namespace MVP.Framework.Core
{
    public interface ILinkDataManager
    {
        void Close(LinkData data);
        void Destroy(LinkData data);
    }

    public class LinkData
    {
        public string                   path;
        public Presenter                presenter;
        public GameObject               prefab;
        public GameObject               node;
        public ILinkDataManager         manager;
        public object                   container;
        public object[]                 result;
        public Subject<Unit>            subject; 
    }

    public static class Utils
    {
        public static LinkData Instantitate(GameObject prefab, string path, ILinkDataManager manager, params object[] args)
        {
            var data = new LinkData();
            data.prefab         = prefab;
            data.manager        = manager;
            data.path           = path;
            data.node           = Object.Instantiate(prefab);
            data.presenter      = Bridge.instance.component.Link(data, args);
            return data;
        }

        public static UnityEngine.Component[] GetComponent(GameObject node, string path)
        {
            path = path.Replace("/", ".");
            return GetComponent(node, Reflection.GetRuntimeType(path));
        }

        public static UnityEngine.Component[] GetComponent(GameObject node, System.Type type) 
        {
            return node.GetComponents(type);
        }
	}
}
