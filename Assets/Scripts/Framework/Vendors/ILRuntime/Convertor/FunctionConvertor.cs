using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace ILRuntime.Convertor
{
    public static class FunctionConvertor
    {
        public static void Register(AppDomain appdomain)
        {
            appdomain.DelegateManager
                .RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance,
                    ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>();

            appdomain.DelegateManager
                .RegisterFunctionDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance,
                    ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>();

            appdomain.DelegateManager.RegisterFunctionDelegate<global::Adapt_IMessage.Adaptor>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.String>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Int32, System.Type>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Single>();
            appdomain.DelegateManager.RegisterFunctionDelegate<UnityEngine.Vector2>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Type, System.String>();
            appdomain.DelegateManager.RegisterFunctionDelegate<global::Adapt_IMessage.Adaptor, global::Adapt_IMessage.Adaptor, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Tuple<System.Single, System.Int32>, System.Tuple<System.Single, System.Int32>, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Tuple<System.Int32, System.Single, System.Int32>, System.Tuple<System.Int32, System.Single, System.Int32>, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<global::EventArgsAdapter.Adapter, global::EventArgsAdapter.Adapter, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Boolean>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Int64, System.Int32, System.Boolean>();
            appdomain.DelegateManager.RegisterFunctionDelegate<System.Collections.Generic.KeyValuePair<System.Int32, System.Int32>, System.Int32>();
            appdomain.DelegateManager.RegisterFunctionDelegate<ILRuntime.CLR.TypeSystem.IType, System.Type>();
        }
    }
}