using System.Collections.Generic;
using MVP.Framework.Core;
using MVP.Framework.Bootstraps.Components;

namespace MVP.Framework.Bootstraps
{
    using LoadingData = Dictionary<string, Views.Proxy>;

    public class Component
    {
        private LoadingData loading;

        public Component()
        {
            loading = new LoadingData(); 
        }

        public void Initialization(Views.Proxy proxy, string view)
        {
            loading.Add(view, proxy);
        }

        public Presenter Link(LinkData context, params object[] args)
        {
            var presenter = CreatePresenter(context, args);
            foreach (var pair in loading)
            {
                var view = CreateView(pair.Value.View);
                view.Initialize(Resolve(presenter, pair.Value));
            }
            presenter.state.Notify();

	        loading.Clear();
            return presenter;
        }

        private View CreateView(string view)
        {
            var path  = Path.instance.Resolve(view, Resource.TYPE.View);
            return Reflection.CreateInstance<View>(path);
        }

        private Presenter CreatePresenter(LinkData data, params object[] args)
        {
            var context     = new Context(data);
            var path  = Path.instance.Resolve(data.path, Resource.TYPE.Presenter);
            return context.Build(path, args);
        }

        private Views.Context Resolve(Presenter presenter, Views.Proxy proxy)
        {
            var data        = new Views.Context();
            data.proxy      = proxy;
            data.presenter  = presenter;
            data.state      = presenter.state;

            return data;
        }
    }
}
