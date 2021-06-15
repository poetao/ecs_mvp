using System;
using System.Collections.Generic;
using Framework.Core;
using Framework.Core.States;
using Framework.Features;

namespace Framework.Bootstraps.Components
{
    public class Context
    {
        public Presenter Build(string path, params object[] args)
        {
            path = Path.instance.Resolve(path, Resource.TYPE.Presenter);
            var instance = Reflection.CreateInstance<Presenter>(path);
            if (instance == null) return null;

            instance.Build(this, args);
            return instance;
        }

        public LinkData                             Data { get; }

        private readonly Dictionary<Type, object>   managers;
        private IState                              state;

        public Context(LinkData data)
        {
            this.Data = data;
            managers = new Dictionary<Type, object>
            {
                { typeof(Scene),        Scene.instance      },
                { typeof(Window),       Window.instance     },
                { typeof(Manager),      Manager.instance    },
            };
        }

        public T GetManager<T>() where T : class
        {
            return managers[typeof(T)] as T;
        }

        public T GetFeature<T>() where T : Feature
        {
            return GetManager<Manager>().GetFeature<T>();
        }

        public void UseState(IState newState)
        {
            this.state = newState;
        }

        public IState TakeState()
        {
            var take = state; state = null;
            return take;
        }
    }
}
