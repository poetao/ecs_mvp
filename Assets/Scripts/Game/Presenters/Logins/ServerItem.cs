using Framework;

namespace Game.Presenters.Logins
{
    using Builder = Framework.Bootstraps.Components.Context;

    public class ServerItemInfo
    {
	    public string name;
	    public string icon;
	    public int    id;
    }

    public class ServerItem : Presenter
    {
		public void SetData(ServerItemInfo info)
		{
			state.Set("title", info.name);
			state.Set("icon", info.icon);
			state.Set("id", info.id);
		}
    }
}
