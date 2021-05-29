using System;
using System.Collections.Generic;

namespace Framework.Core.States
{
    public interface IState
    {
        void Set<T>(string path, T value);
        T Get<T>(string path);
        IDisposable Subscribe(string path, IObserver<WrapBase> observer);
        IObservable<WrapBase> GetObservable(string path = "");
        void Notify();
        IDictionary<string, WrapBase> GetRaw();
    }
}
