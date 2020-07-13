using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters.Framework
{
	using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Root : Presenter
    {
		protected override void Create(Context context, Builder builder, params object[] args)
	    {
            base.Create(context, builder, args);
	    }
    }
}
