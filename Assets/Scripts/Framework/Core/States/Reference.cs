using System;
using System.Collections.Generic;

namespace MVP.Framework.Core.States
{
    [Serializable]
    public class Reference : IState
    {
        private State state;
        private string path;

        public Reference(State state, string path)
        {
            this.state = state;
            this.path  = path;
        }

        public void Set<T>(string path, T value)
        {
            this.state.Set(map(path), value);
        }

        public T Get<T>(string path)
        {
            return this.state.Get<T>(map(path));
        }

        public IObservable<WrapBase> GetObservable(string path)
        {
            return this.state.GetObservable(map(path));
        }

        public IDisposable Subscribe(string path, IObserver<WrapBase> observer)
        {
            return this.state.Subscribe(map(path), observer);
        }

        public void Notify()
        {
            this.state.Notify();
        }

        private string map(string path)
        {
            if (string.IsNullOrEmpty(path)) return this.path;
            return $"{this.path}.{path}";
        }

        public IDictionary<string, WrapBase> GetRaw()
        {
            return state.GetRaw();
        }
    }
}
