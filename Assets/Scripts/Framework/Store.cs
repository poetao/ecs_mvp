using System;
using Framework.Core.States;

namespace Framework
{
    public class Store
    {
        public static Store instance { get; private set; }

        public Core.State state { get; private set; }
        
        public static void Setup()
        {
            instance = new Store();
        }

        private Store()
        {
            state = new Core.State();
        }

        public void Set<T>(string path, T value)
        {
            state.Set(path, value);
        }

        public T Get<T>(string path)
        {
            return state.Get<T>(path);
        }

        public Reference Reference(string path)
        {
            return new Reference(state, path);
        }

        public IObservable<WrapBase> GetObservable(string path)
        {
            return state.GetObservable(path);
        }

        public IState GetState()
        {
            return state;
        }
    }
}
