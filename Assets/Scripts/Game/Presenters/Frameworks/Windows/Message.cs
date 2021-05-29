using Framework;
using Framework.Presenters;

namespace Game.Presenters.Frameworks.Windows
{
    using Builder = Framework.Bootstraps.Components.Context;

    public class Message : Presenter
    {
		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);

			var text = args[0] as string;
		    context.state.Set("text", text);
	    }
    }
}
