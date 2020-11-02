using UnityEngine;
using UniRx;
using MVP.Framework.Bootstraps;

namespace MVP.Framework.Core
{
    using Container = MVP.Framework.Components.Windows.Container;
    public interface ILinkDataManager
    {
        void Close(LinkData data);
        void Destroy(LinkData data);
    }

    public class LinkData
    {
        public string                   path;
        public Presenter                presenter;
        public GameObject               node;
        public ILinkDataManager         manager;
        public Container                container;
        public object[]                 result;
        public Subject<Unit>            subject;

        public void Destroy()
        {
            this.presenter.Dispose();
            this.subject.OnNext(Unit.Default);
            this.subject.OnCompleted();
            this.subject.Dispose();

            this.path = null;
            this.presenter = null;
            this.node = null;
            this.container = null;
            this.result = null;
            this.subject = null;
        }
    }

    public static class Utils
    {
        public static LinkData Instantitate(Resources.AssetRef prefab, string path, ILinkDataManager manager, params object[] args)
        {
            var data = new LinkData();
            data.manager        = manager;
            data.path           = path;
            data.node           = Resource.instance.Instantiate(prefab);
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
