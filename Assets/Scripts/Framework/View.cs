using MVP.Framework.Views;

namespace MVP.Framework
{
    public class View
    {
        public void Initialize(Context context)
        {
            BuildLinks(context);
            BuildSlots(context);
	        context.proxy.DestroySelf();
        }

        private void BuildLinks(Context context)
        {
            for (int i = 0; i < context.proxy.linkProperties.Length; ++i)
            {
                var name        = context.proxy.linkProperties[i];
                var component   = context.proxy.linkComponents[i];
                Link.Bind(component, name, context.state, this);
            }
        }

        private void BuildSlots(Context context)
        {
            for (int i = 0; i < context.proxy.slotProperties.Length; ++i)
            {
                var name        = context.proxy.slotProperties[i];
                var component   = context.proxy.slotComponents[i];
                var parameters = context.proxy.slotParameters[i].array;
                var throttle = i < context.proxy.slotThrottles.Length ? context.proxy.slotThrottles[i] : 0;
                Slot.Bind(component, name, context.presenter, parameters, throttle);
            }
        }
    }
}
