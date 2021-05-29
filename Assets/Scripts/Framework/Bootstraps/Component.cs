using System.Collections.Generic;
using Framework.Core;

namespace Framework.Bootstraps
{
    using LIST_PROXY = List<Views.Proxy>;
    using LIST_MONO  = List<Framework.Component>;

    public class Component
    {
        private LIST_PROXY loadings;

        public Component()
        {
            loadings = new LIST_PROXY();
        }

        public void Initialization(Views.Proxy proxy)
        {
            loadings.Add(proxy);
        }

        public Presenter Link(LinkData context, params object[] args)
        {
            var loadingProxys = loadings;
            loadings = new LIST_PROXY();

            var presenter = CreatePresenter(context, args);
            foreach (var proxy in loadingProxys)
            {
                if (proxy.isComponent)
                {
                    CreateComponent(presenter, proxy);
                }
                else
                {
                    CreateView(presenter, proxy);
                }
            }
            presenter.Notify();

	        loadingProxys.Clear();
            return presenter;
        }

        private void CreateComponent(Presenter presenter, Views.Proxy proxy)
        {
            var path  = Path.instance.Resolve(proxy.path, Resource.TYPE.Component, proxy.assembly);
            var component  = Reflection.CreateInstance<Framework.Component>(path, proxy.assembly);
            component.Create(ResolveContext(presenter, proxy));
        }

        private void CreateView(Presenter presenter, Views.Proxy proxy)
        {
            var path  = Path.instance.Resolve(proxy.path, Resource.TYPE.View);
            var view  = Reflection.CreateInstance<View>(path);
            view.Create(ResolveContext(presenter, proxy));
        }

        private Presenter CreatePresenter(LinkData data, params object[] args)
        {
            var context     = new Components.Context(data);
            return context.Build(data.path, args);
        }

        private Views.Context ResolveContext(Presenter presenter, Views.Proxy proxy)
        {
            var data        = new Views.Context();
            data.proxy      = proxy;
            data.presenter  = presenter.Refrence(proxy.presenterRef);
            data.state      = data.presenter.state;
            data.gameObject = proxy.gameObject;

            return data;
        }
    }
}
