using System;
using UnityEngine;

namespace UniRx.Triggers
{
    [DisallowMultipleComponent]
    public class ObservableViewCloseTrigger : MonoBehaviour
    {
        bool calledDestroy = false;
        Subject<object[]> onDestroy;
        CompositeDisposable disposablesOnDestroy;

        public object[] Result { get; set; }

        [Obsolete("Internal Use.")]
        internal bool IsMonitoredActivate { get; set; }

        public bool IsActivated { get; private set; }

        /// <summary>
        /// Check called OnDestroy.
        /// This property does not guarantees GameObject was destroyed,
        /// when gameObject is deactive, does not raise OnDestroy.
        /// </summary>
        public bool IsCalledOnDestroy { get { return calledDestroy; } }

        void Awake()
        {
            IsActivated = true;
        }

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        void OnDestroy()
        {
            if (!calledDestroy)
            {
                calledDestroy = true;
                if (disposablesOnDestroy != null) disposablesOnDestroy.Dispose();
                if (onDestroy != null) { onDestroy.OnNext(Result); onDestroy.OnCompleted(); }
            }
        }

        /// <summary>This function is called when the MonoBehaviour will be destroyed.</summary>
        public IObservable<object[]> OnDestroyAsObservable()
        {
            return onDestroy ?? (onDestroy = new Subject<object[]>());
        }

        /// <summary>Invoke OnDestroy, this method is used on internal.</summary>
        public void ForceRaiseOnDestroy()
        {
            OnDestroy();
        }

        public void AddDisposableOnDestroy(IDisposable disposable)
        {
            if (calledDestroy)
            {
                disposable.Dispose();
                return;
            }

            if (disposablesOnDestroy == null) disposablesOnDestroy = new CompositeDisposable();
            disposablesOnDestroy.Add(disposable);
        }
    }
}
