using System;
using UniRx;
using Framework.Core;
using Framework.Core.States;
using Framework.Features;

namespace Framework
{
    public abstract class Feature : IDisposable
    {
        static private bool useAutoSave = false;

        protected Storage       storage;
        protected Store         store;
        protected IState        state;
        private   CompositeDisposable disposables;

        public virtual void Create(Manager manager, IState state, params object[] args)
        {
            store       = manager.GetManager<Store>();
            storage     = manager.GetManager<Storage>();
            disposables = new CompositeDisposable();

            UseState(state);
            UseAutoSave();
        }

        public virtual void Dispose()
        {
            disposables.Dispose();
        }

        public IObservable<T> GetObservable<T>(string path = "")
        {
            return GetObservable(path).Select(x =>
            {
                if (x == WrapBase.Empty)
                {
                    return default(T);
                }

                return x.ValueOf<T>();
            });
        }

        public IObservable<WrapBase> GetObservable(string path = "")
        {
            return state.GetObservable(path);
        }

        public IState GetState()
        {
            return state;
        }

        public IState Reference(string path)
        {
            return new Reference(state, path);
        }

        protected virtual void AutoSave()
        {
            storage.SaveBySerialize($"{this.GetType().FullName}", state);
        }

        private void UseState(IState state)
        {
            var reference = state as Reference;
            if (reference != null)
            {
                this.state = reference.GetState();
                if (this.state == null)
                {
                    Log.Startup.I("{0} Get Real State Fail, Continue Use Reference", this);
                    this.state = state;
                }
                return;
            }

            this.state = state;
        }

        private void UseAutoSave()
        {
            if (!useAutoSave) return;

            state.GetObservable().Subscribe(x => AutoSave()).AddTo(disposables);
        }
    }
}
