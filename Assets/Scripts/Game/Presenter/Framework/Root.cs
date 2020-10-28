namespace Presenters.Framework
{
	using Context = MVP.Framework.Presenters.Context;
	using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Root : MVP.Framework.Presenter
    {
		protected override void Create(Context context, Builder builder, params object[] args)
	    {
            base.Create(context, builder, args);
	    }
    }
}
