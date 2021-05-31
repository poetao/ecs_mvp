using System;

namespace Framework.Core.States
{
    public interface IState
    {
        WrapBase Get(string path);
        T Get<T>(string path);
        void Set<T>(string path, T value);
        IObservable<WrapBase> GetObservable(string path = "");
        IDisposable Subscribe(string path, IObserver<WrapBase> observer);
        void Notify(string path = "");
    }
}
