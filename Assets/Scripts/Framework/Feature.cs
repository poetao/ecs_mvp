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
            this.store       = manager.GetManager<Store>();
            this.storage     = manager.GetManager<Storage>();
            this.state       = state;
            this.disposables = new CompositeDisposable();

            if (useAutoSave)
            {
                this.state.GetObservable().Subscribe(x => AutoSave()).AddTo(disposables);
            }
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

        protected virtual void AutoSave()
        {
            storage.SaveBySerialize($"{this.GetType().FullName}", state.GetRaw());
        }
    }
}
