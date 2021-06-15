using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;public class IPointerEnterHandlerAdapter : CrossBindingAdaptor
{
        static CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData> mOnPointerEnter_0 = new CrossBindingMethodInfo<UnityEngine.EventSystems.PointerEventData>("OnPointerEnter");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityEngine.EventSystems.IPointerEnterHandler);
        }
    }

    public override Type AdaptorType
    {
        get
        {
            return typeof(Adapter);
        }
    }

    public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
    {
        return new Adapter(appdomain, instance);
    }

    public class Adapter : UnityEngine.EventSystems.IPointerEnterHandler, CrossBindingAdaptorType
    {
        ILTypeInstance instance;
        ILRuntime.Runtime.Enviorment.AppDomain appdomain;

        public Adapter()
        {

        }

        public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            this.appdomain = appdomain;
            this.instance = instance;
        }

        public ILTypeInstance ILInstance { get { return instance; } }

            public void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
            {
                mOnPointerEnter_0.Invoke(this.instance, eventData);
            }

        public override string ToString()
        {
            IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
            m = instance.Type.GetVirtualMethod(m);
            if (m == null || m is ILMethod)
            {
                return instance.ToString();
            }
            else
                return instance.Type.FullName;
        }
    }
}

