using System;
using System.Collections.Generic;

namespace Framework.Core.States
{
    [Serializable]
    public class Reference : IState
    {
        private IState state;
        private string path;

        public Reference(IState state, string path)
        {
            this.state = state;
            this.path  = path;
        }

        public void Set<T>(string path, T value)
        {
            this.state.Set(map(path), value);
        }

        public WrapBase Get(string path)
        {
            return this.state.Get(map(path));
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

        public void Notify(string path)
        {
            this.state.Notify(map(path));
        }

        public IState GetState()
        {
            if (string.IsNullOrEmpty(path)) return state;
            return state.Get<IState>(path);
        }

        private string map(string path)
        {
            if (string.IsNullOrEmpty(path)) return this.path;
            return $"{this.path}.{path}";
        }
    }
}
