using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

public class ILayoutElementAdapter : CrossBindingAdaptor
{
    static CrossBindingMethodInfo mCalculateLayoutInputHorizontal_0 = new CrossBindingMethodInfo("CalculateLayoutInputHorizontal");
    static CrossBindingMethodInfo mCalculateLayoutInputVertical_1 = new CrossBindingMethodInfo("CalculateLayoutInputVertical");
    static CrossBindingFunctionInfo<System.Single> mget_minWidth_2 = new CrossBindingFunctionInfo<System.Single>("get_minWidth");
    static CrossBindingFunctionInfo<System.Single> mget_preferredWidth_3 = new CrossBindingFunctionInfo<System.Single>("get_preferredWidth");
    static CrossBindingFunctionInfo<System.Single> mget_flexibleWidth_4 = new CrossBindingFunctionInfo<System.Single>("get_flexibleWidth");
    static CrossBindingFunctionInfo<System.Single> mget_minHeight_5 = new CrossBindingFunctionInfo<System.Single>("get_minHeight");
    static CrossBindingFunctionInfo<System.Single> mget_preferredHeight_6 = new CrossBindingFunctionInfo<System.Single>("get_preferredHeight");
    static CrossBindingFunctionInfo<System.Single> mget_flexibleHeight_7 = new CrossBindingFunctionInfo<System.Single>("get_flexibleHeight");
    static CrossBindingFunctionInfo<System.Int32> mget_layoutPriority_8 = new CrossBindingFunctionInfo<System.Int32>("get_layoutPriority");
    public override Type BaseCLRType
    {
        get
        {
            return typeof(UnityEngine.UI.ILayoutElement);
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

    public class Adapter : UnityEngine.UI.ILayoutElement, CrossBindingAdaptorType
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

        public void CalculateLayoutInputHorizontal()
        {
            mCalculateLayoutInputHorizontal_0.Invoke(this.instance);
        }

        public void CalculateLayoutInputVertical()
        {
            mCalculateLayoutInputVertical_1.Invoke(this.instance);
        }

        public System.Single minWidth
        {
        get
        {
            return mget_minWidth_2.Invoke(this.instance);

        }
        }

        public System.Single preferredWidth
        {
        get
        {
            return mget_preferredWidth_3.Invoke(this.instance);

        }
        }

        public System.Single flexibleWidth
        {
        get
        {
            return mget_flexibleWidth_4.Invoke(this.instance);

        }
        }

        public System.Single minHeight
        {
        get
        {
            return mget_minHeight_5.Invoke(this.instance);

        }
        }

        public System.Single preferredHeight
        {
        get
        {
            return mget_preferredHeight_6.Invoke(this.instance);

        }
        }

        public System.Single flexibleHeight
        {
        get
        {
            return mget_flexibleHeight_7.Invoke(this.instance);

        }
        }

        public System.Int32 layoutPriority
        {
        get
        {
            return mget_layoutPriority_8.Invoke(this.instance);

        }
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

