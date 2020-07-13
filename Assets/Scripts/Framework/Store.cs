using System;
using MVP.Framework.Core;
using MVP.Framework.Core.States;

namespace MVP.Framework
{
    public class Store
    {
        public static Store instance { get; private set; }

        public State state { get; private set; }
        
        public static void Setup()
        {
            instance = new Store();
        }

        private Store()
        {
            state = new State();
        }

        public void Set<T>(string path, T value)
        {
            state.Set(path, value);
        }

        public void Set(string path, Any value)
        {
            state.Set(path, value);
        }

        public T Get<T>(string path)
        {
            return state.Get<T>(path);
        }

        public Any Get(string path)
        {
            return state.Get(path);
        }

        public Reference Reference(string path)
        {
            return new Reference(state, path);
        }

        public IObservable<Any> GetObservable(string path)
        {
            return state.GetObservable(path);
        }

        public IState GetState()
        {
            return state;
        }
    }
}