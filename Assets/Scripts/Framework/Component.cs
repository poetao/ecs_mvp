using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MVP.Framework.Views;
using Context = MVP.Framework.Views.Context;

namespace MVP.Framework
{
    using WEAK_COMP_DIC     = Dictionary<int, Dictionary<Type, WeakReference<Component>>>;

    public class Component
    {
        public     static WEAK_COMP_DIC peers = new WEAK_COMP_DIC();
        protected         Context       context;

        public static WeakReference<Component> GetPeer(GameObject gameObject, Type type)
        {
            var hashCode = gameObject.GetHashCode();
            if (!peers.ContainsKey(hashCode)) return null;

            var compDic = peers[hashCode];
            if (!compDic.ContainsKey(type)) return null;

            return compDic[type];
        }

        public virtual void Create(Context context)
        {
            this.context = context;
            this.context.gameObject.OnDestroyAsObservable().Subscribe(x => Dispose());

            BuildInspectors(context);
        }

        protected virtual void Dispose()
        {
        }

        private void BuildInspectors(Context context)
        {
            foreach (var inspectorItem in context.proxy.inspectorItems)
            {
                var name      = inspectorItem.name;
                var param     = inspectorItem.parameter;
                Inspector.Bind(name, param, this);
            }
        }

    }
}