using ILRuntime.Runtime.Enviorment;
using ILRuntime.Convertor;
using ILRuntime.Redirection;
using Framework.Vendors.ILRuntime.Redirectors;

namespace Framework.Vendors.ILRuntime
{
    public static class Loader
    {
        public static void InitializeILRuntime(AppDomain domain)
        {
    #if DEBUG && !NO_PROFILER
            //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
            domain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
    #endif

            RegisterCrossBindingAdaptor(domain);
            SetupCLRRedirection(domain);
            RegisterConvertor(domain);
            // Runtime.Generated.CLRBindings.Initialize(domain);
        }

        public static void RegisterCrossBindingAdaptor(AppDomain domain)
        {
            domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
            domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
            domain.RegisterCrossBindingAdaptor(new EventArgsAdapter());
            domain.RegisterCrossBindingAdaptor(new ILayoutElementAdapter());
            domain.RegisterCrossBindingAdaptor(new WebClientAdapter());
            domain.RegisterCrossBindingAdaptor(new Adapt_IMessage());
            domain.RegisterCrossBindingAdaptor(new IPointerClickHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IPointerDownHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IPointerUpHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IBeginDragHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IEndDragHandlerAdapter()); 
            domain.RegisterCrossBindingAdaptor(new IDragHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IPointerEnterHandlerAdapter()); 
            domain.RegisterCrossBindingAdaptor(new IPointerExitHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new IDeselectHandlerAdapter());
            domain.RegisterCrossBindingAdaptor(new EventTriggerAdapter());
            domain.RegisterCrossBindingAdaptor(new UnityEvent_1_ObjectAdapter());
            domain.RegisterCrossBindingAdaptor(new IAsyncStateMachineClassInheritanceAdaptor());
            domain.RegisterCrossBindingAdaptor(new TextAdapter());
            domain.RegisterCrossBindingAdaptor(new UnityEvent_1_StringAdapter());
        }

        static void SetupCLRRedirection(AppDomain domain)
        {
            ReGameObject.SetupCLRRedirection(domain);
            ReActivator.SetupCLRRedirection(domain);
            ReList.Register(domain);
            LitJson.JsonMapper.RegisterILRuntimeCLRRedirection(domain);
        }

        static void RegisterConvertor(AppDomain domain)
        {
            FunctionConvertor.Register(domain);
            MethodConvertor.Register(domain);
            DelegateConvertor.Register(domain);
        }

        public static void OnHotFixLoaded(AppDomain appdomain, params object[] args)
        {
            // 调用热更Dll中的GameInitiator挂在或者直接调用函数
            appdomain.Invoke("GameInitiator", "Startup", null, args);
        }

        private static void ForbidIL2CppTripNeverCall()
        {
            System.Func<bool> d = () => { return true; };
            d.DynamicInvoke();
        }
    }
}