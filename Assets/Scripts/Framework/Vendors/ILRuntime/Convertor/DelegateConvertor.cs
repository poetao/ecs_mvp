using System;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

namespace ILRuntime.Convertor
{
    public static class DelegateConvertor
    {
        public static void Register(AppDomain appdomain)
        {
            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>>((act) =>
            {
                return new System.Comparison<ILRuntime.Runtime.Intepreter.ILTypeInstance>((x, y) =>
                {
                    return ((Func<ILRuntime.Runtime.Intepreter.ILTypeInstance, ILRuntime.Runtime.Intepreter.ILTypeInstance, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.EventHandler<global::EventArgsAdapter.Adapter>>((act) =>
            {
                return new System.EventHandler<global::EventArgsAdapter.Adapter>((sender, e) =>
                {
                    ((Action<System.Object, global::EventArgsAdapter.Adapter>)act)(sender, e);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction>((act) =>
            {
                return new UnityEngine.Events.UnityAction(() =>
                {
                    ((Action)act)();
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.String>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.String>((arg0) =>
                {
                    ((Action<System.String>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Threading.TimerCallback>((act) =>
            {
                return new System.Threading.TimerCallback((state) =>
                {
                    ((Action<System.Object>)act)(state);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.AsyncCallback>((act) =>
            {
                return new System.AsyncCallback((ar) =>
                {
                    ((Action<System.IAsyncResult>)act)(ar);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<UnityEngine.EventSystems.BaseEventData>((arg0) =>
                {
                    ((Action<UnityEngine.EventSystems.BaseEventData>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Single>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Single>((arg0) =>
                {
                    ((Action<System.Single>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<global::Adapt_IMessage.Adaptor>>((act) =>
            {
                return new System.Comparison<global::Adapt_IMessage.Adaptor>((x, y) =>
                {
                    return ((Func<global::Adapt_IMessage.Adaptor, global::Adapt_IMessage.Adaptor, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Tuple<System.Single, System.Int32>>>((act) =>
            {
                return new System.Comparison<System.Tuple<System.Single, System.Int32>>((x, y) =>
                {
                    return ((Func<System.Tuple<System.Single, System.Int32>, System.Tuple<System.Single, System.Int32>, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<System.Tuple<System.Int32, System.Single, System.Int32>>>((act) =>
            {
                return new System.Comparison<System.Tuple<System.Int32, System.Single, System.Int32>>((x, y) =>
                {
                    return ((Func<System.Tuple<System.Int32, System.Single, System.Int32>, System.Tuple<System.Int32, System.Single, System.Int32>, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<System.Object>>((act) =>
            {
                return new UnityEngine.Events.UnityAction<System.Object>((arg0) =>
                {
                    ((Action<System.Object>)act)(arg0);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<System.Comparison<global::EventArgsAdapter.Adapter>>((act) =>
            {
                return new System.Comparison<global::EventArgsAdapter.Adapter>((x, y) =>
                {
                    return ((Func<global::EventArgsAdapter.Adapter, global::EventArgsAdapter.Adapter, System.Int32>)act)(x, y);
                });
            });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Events.UnityAction<UnityEngine.Vector2>>((act) =>
            {
              return new UnityEngine.Events.UnityAction<UnityEngine.Vector2>((arg0) =>
                {
                    ((Action<UnityEngine.Vector2>)act)(arg0); });
             });

            appdomain.DelegateManager.RegisterDelegateConvertor<UnityEngine.Video.VideoPlayer.EventHandler>((act) =>
            {
                return new UnityEngine.Video.VideoPlayer.EventHandler((source) =>
                {
                    ((Action<UnityEngine.Video.VideoPlayer>)act)(source);
                });
            });
        }
    }
}