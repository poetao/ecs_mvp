using System;

namespace MVP.Framework.Core.States
{
    public interface IState
    {
        void Default<T>(string path, T value);
        void Default(string path, Any value);
        void Set<T>(string path, T value, bool forceNotify = false);
        void Set(string path, Any value, bool forceNotify = false);
        T Get<T>(string path);
        Any Get(string path);
        IObservable<Any> GetObservable(string path = "");
        IDisposable Subscribe(string path, IObserver<Any> observer);
        void Notify();
        object GetRaw();
    }
}