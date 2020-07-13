using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters.Stages
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Level2 : Presenter
    {
        private Window window;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);

			window = builder.GetManager<Window>();
	    }
    }
}
