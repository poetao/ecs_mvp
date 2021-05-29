using System;
using System.Collections.Generic;
using UniRx;
using Framework.Core;
using Framework.Core.States;
using Framework.Presenters;

namespace Framework
{
    using Builder = Bootstraps.Components.Context;

    public class Presenter : IDisposable
    {
		public		IState	state { get; private set; }
        public      CompositeDisposable disposables;
        protected	Context context;
        protected   Dictionary<string, Presenter> subPresenters;

	    public void Build(Builder builder, params object[] args)
	    {
		    try
		    {
			    Create(Context.Create(builder), builder, args);
		    }
		    catch (Exception e)
		    {
			    Log.Framework.Exception(e);
		    }
	    }

	    public virtual void Dispose()
	    {
		    foreach (var subPresenter in subPresenters)
		    {
			    subPresenter.Value.Dispose();
		    }
		    subPresenters.Clear();

		    if (disposables != null) disposables.Dispose();
	    }

	    public Presenter Refrence(string path)
	    {
		    if (subPresenters.ContainsKey(path))
		    {
			    return subPresenters[path];
		    }

		    return this;
	    }

	    public void Notify()
	    {
		    foreach (var pairs in subPresenters)
		    {
			    pairs.Value.Notify();
		    }
		    this.state.Notify();
	    }

	    public void Close(params object[] args)
	    {
		    context.Close(args);
	    }

		protected virtual void Create(Context context, Builder builder, params object[] args)
        {
	        this.subPresenters = new Dictionary<string, Presenter>(); 
            this.context	   = context;
			this.state		   = context.state;
        }
    }

    public static class DisposableExtensions
    {
	    public static T AddTo<T>(this T disposable, Presenter presenter) where T : IDisposable
	    {
		    if (presenter.disposables == null) presenter.disposables = new CompositeDisposable();
		    presenter.disposables.Add(disposable);
		    return disposable;
	    }
    }
}
