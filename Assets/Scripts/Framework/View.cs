using UnityEngine;
using UniRx;
using UniRx.Triggers;
using MVP.Framework.Views;

namespace MVP.Framework
{
    public class View
    {
        public GameObject gameObject { get; private set; }

        public void Create(Context context)
        {
            gameObject = context.gameObject;
            gameObject.OnDestroyAsObservable().Subscribe(x => Dispose());

            BuildLinks(context);
            BuildSlots(context);
            context.proxy.DestroySelf();
        }

        private void Dispose()
        {
        }

        private void BuildLinks(Context context)
        {
            foreach (var linkItem in context.proxy.linkItems)
            {
                var name = linkItem.name;
                var component = linkItem.gameObject;
                var compType = linkItem.componentType;
                Link.Bind(component, compType, name, context.state, this);
            }
        }

        private void BuildSlots(Context context)
        {
            foreach (var slotItem in context.proxy.slotItems)
            {
                var name = slotItem.name;
                var component = slotItem.gameObject;
                var parameters = slotItem.parameters;
                var throttle = slotItem.throttle;
                Slot.Bind(component, name, context.presenter, parameters, throttle);
            }
        }
    }
}
