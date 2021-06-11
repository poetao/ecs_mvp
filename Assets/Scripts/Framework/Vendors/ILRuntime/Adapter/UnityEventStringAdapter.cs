using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class UnityEvent_1_StringAdapter : CrossBindingAdaptor
{
    static CrossBindingFunctionInfo<System.String, System.Object, System.Reflection.MethodInfo> mFindMethod_Impl_0 = new CrossBindingFunctionInfo<System.String, System.Object, System.Reflection.MethodInfo>("FindMethod_Impl");
    // static CrossBindingFunctionInfo<System.Object, System.Reflection.MethodInfo, UnityEngine.Events.BaseInvokableCall> mGetDelegate_1 = new CrossBindingFunctionInfo<System.Object, System.Reflection.MethodInfo, UnityEngine.Events.BaseInvokableCall>("GetDelegate");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityEngine.Events.UnityEvent<System.String>);
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

    public class Adapter : UnityEngine.Events.UnityEvent<System.String>, CrossBindingAdaptorType
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

        protected override System.Reflection.MethodInfo FindMethod_Impl(System.String name, System.Object targetObj)
        {
            if (mFindMethod_Impl_0.CheckShouldInvokeBase(this.instance))
                return base.FindMethod_Impl(name, targetObj);
            else
                return mFindMethod_Impl_0.Invoke(this.instance, name, targetObj);
        }

        // public override UnityEngine.Events.BaseInvokableCall GetDelegate(System.Object target, System.Reflection.MethodInfo theFunction)
        // {
        //     if (mGetDelegate_1.CheckShouldInvokeBase(this.instance))
        //         return base.GetDelegate(target, theFunction);
        //     else
        //         return mGetDelegate_1.Invoke(this.instance, target, theFunction);
        // }

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

