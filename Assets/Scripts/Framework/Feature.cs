using System;
using UniRx;
using MVP.Framework.Core;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace MVP.Framework
{
    public abstract class Feature : IDisposable
    {
        protected Storage       storage;
        protected Store         store;
        protected IState        state;
        private   CompositeDisposable disposables;

        public virtual void Create(Manager manager, IState state, params object[] args)
        {
            this.store       = manager.GetManager<Store>();
            this.storage     = manager.GetManager<Storage>();
            this.state       = state;
            this.disposables = new CompositeDisposable();

            this.state.GetObservable().Subscribe(x => AutoSave()).AddTo(disposables);
        }

        public virtual void Dispose()
        {
            disposables.Dispose();
        }

        public T Get<T>(string path = "") where T : IEquatable<T>
        {
            return state.Get<T>(path);
        }

        public IObservable<WrapBase> GetObservable(string path = "")
        {
            return state.GetObservable(path);
        }

        public void Notify()
        {
            state.Notify();
        }

        protected virtual void AutoSave()
        {
            storage.SaveBySerialize($"{this.GetType().FullName}", state.GetRaw());
        }
    }
}
