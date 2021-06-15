using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Framework.Views;
using Context = Framework.Views.Context;

namespace Framework
{
    using WEAK_COMP_DIC     = Dictionary<int, Dictionary<Type, WeakReference<Component>>>;

    public class Component
    {
        public     static WEAK_COMP_DIC peers = new WEAK_COMP_DIC();
        protected         Context       context;

        public static Component GetPeer(GameObject gameObject, Type type)
        {
            var hashCode = gameObject.GetHashCode();
            if (!peers.ContainsKey(hashCode)) return null;

            var compDic = peers[hashCode];
            if (!compDic.ContainsKey(type)) return null;

            Component component; compDic[type].TryGetTarget(out component);
            return component;
        }

        public static void SetPeer(GameObject gameObject, Component component)
        {
            var hashCode = gameObject.GetHashCode();
            if (!peers.ContainsKey(hashCode))
            {
                peers.Add(hashCode, new Dictionary<Type, WeakReference<Component>>());
            }

            var compDic = peers[hashCode];
            compDic[component.GetType()] = new WeakReference<Component>(component);
        }

        public virtual void Create(Context context)
        {
            this.context = context;
            this.context.gameObject.OnDestroyAsObservable().Subscribe(x => Dispose());

            SetPeer(this.context.gameObject, this);
            BuildInspectors(context);
        }

        protected virtual void Dispose()
        {
        }

        private void BuildInspectors(Context context)
        {
            foreach (var inspectorItem in context.proxy.parameterItems)
            {
                var name      = inspectorItem.name;
                var param     = inspectorItem.parameter;
                Inspector.Bind(name, param, this);
            }
        }

    }
}