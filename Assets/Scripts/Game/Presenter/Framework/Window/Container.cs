using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters.Framework.Window
{
	using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Container : Presenter
    {
		protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
        }
    }
}
