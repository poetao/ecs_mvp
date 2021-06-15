using ILRuntime.Runtime.Enviorment;

namespace Framework.Vendors.ILRuntimes.Convertor
{
    public static class MethodConvertor
    {
        public static void Register(AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterMethodDelegate<System.Object, global::EventArgsAdapter.Adapter>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.String, System.Action<UnityEngine.Object>>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.String>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Object>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.IAsyncResult>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.Int32>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Boolean>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Int32>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Single>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Int64>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Vector2>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.GameObject>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.EventSystems.BaseEventData>();
            appdomain.DelegateManager.RegisterMethodDelegate<ILRuntime.Runtime.Intepreter.ILTypeInstance>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Int32, System.Int64>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.GameObject>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Byte[]>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Font>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Type, System.Int32>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Object, System.EventArgs>();
            appdomain.DelegateManager.RegisterMethodDelegate<System.Boolean, System.Int32>();
            appdomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Video.VideoPlayer>();
            appdomain.DelegateManager.RegisterMethodDelegate<UniRx.Unit>();
        }
    }
}