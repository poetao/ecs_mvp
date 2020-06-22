using System;

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

        public void Default(string path, Any value)
        {
            var castPath = map(path);
            if (state.Has(castPath)) return;

            this.state.Set(castPath, value);
        }

        public void Default<T>(string path, T value)
        {
            var castPath = map(path);
            if (state.Has(castPath)) return;

            this.state.Set(castPath, value);
        }

        public void Set(string path, Any value, bool forceNotify = false)
        {
            this.state.Set(map(path), value, forceNotify);
        }

        public void Set<T>(string path, T value, bool forceNotify = false)
        {
            this.state.Set(map(path), value, forceNotify);
        }

        public Any Get(string path)
        {
            return this.state.Get(map(path));
        }

        public T Get<T>(string path)
        {
            return this.state.Get<T>(map(path));
        }

        public IObservable<Any> GetObservable(string path)
        {
            return this.state.GetObservable(map(path));
        }

        public IDisposable Subscribe(string path, IObserver<Any> observer)
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

        public object GetRaw()
        {
            return state.GetRaw();
        }
    }
}