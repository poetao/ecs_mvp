using System;
using System.Collections;
using UnityEngine;
using UniRx;
using MVP.Framework.Core.States;
using MVP.Framework.Presenters;

namespace MVP.Framework
{
    using Builder = Bootstraps.Components.Context;

    public class Presenter : IDisposable
    {
		public		IState	state { get; private set; }
        public      CompositeDisposable disposables;
        protected	Context context;

        public Presenter() {}

	    public void Build(Builder builder, params object[] args)
	    {
	        Create(Context.Create(builder), builder, args);
	    }

	    public virtual void Dispose()
	    {
		    if (disposables != null) disposables.Dispose();
	    }

		protected virtual void Create(Context context, Builder builder, params object[] args)
        {
            this.context	= context;
			this.state		= context.state;
        }
    }

    public static partial class DisposableExtensions
    {
	    public static T AddTo<T>(this T disposable, Presenter presenter) where T : IDisposable
	    {
		    if (presenter.disposables == null) presenter.disposables = new CompositeDisposable();
		    presenter.disposables.Add(disposable);
		    return disposable;
	    }
    }
}
