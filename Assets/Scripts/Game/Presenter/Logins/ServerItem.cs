using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters.Logins
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class ServerItemInfo
    {
	    public string name;
	    public string icon;
	    public int    id;
    }

    public class ServerItem : Presenter
    {
        private Window window;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);
			window = builder.GetManager<Window>();
	    }

		public void SetData(ServerItemInfo info)
		{
			state.Set("title", info.name);
			state.Set("icon", info.icon);
			state.Set("id", info.id);
		}
    }
}
