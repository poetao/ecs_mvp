using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters.Framework.Windows
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Toast : Presenter
    {
        private Window window;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);

			window = builder.GetManager<Window>();
	    }
    }
}
