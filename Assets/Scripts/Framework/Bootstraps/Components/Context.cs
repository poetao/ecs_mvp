using System;
using System.Collections.Generic;
using UnityEngine;
using MVP.Framework.Core;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace MVP.Framework.Bootstraps.Components
{
    public class Context
    {
        public Presenter Build(string path, params object[] args)
        {
            var instance = Reflection.CreateInstance<Presenter>(path);
            if (instance == null) return null;

            instance.Build(this, args);
            return instance;
        }

        public LinkData                     data { get; private set; }

        private Dictionary<Type, object>    managers;
        private IState                      state;

        public Context(LinkData data)
        {
            this.data = data;
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

        public void UseState(IState state)
        {
            this.state = state;
        }

        public IState TakeState()
        {
            var take = state; state = null;
            return take;
        }
    }
}
