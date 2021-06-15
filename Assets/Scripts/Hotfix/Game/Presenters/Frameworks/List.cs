using System;
using System.Collections.Generic;
using Framework;
using Framework.Core;
using Framework.Resources;

namespace Game.Presenters.Frameworks
{
	using Context = Framework.Presenters.Context;
	using Builder = Framework.Bootstraps.Components.Context;

	public class LIST_INFO
	{
		public IEnumerable<object> list;
		public Action<Presenter, object> action;
	}

    public class List : Presenter, ILinkDataManager
    {
	    private object[] args;
		protected override void Create(Context context, Builder builder, params object[] args)
        {
			base.Create(context, builder, args);
			this.args = args;
        }

		public void Update(LIST_INFO data)
		{
			state.Set("data", data);
		}

		public LinkData Build(AssetRef prefab, String presenter)
		{
			return Utils.Instantitate(prefab, presenter, this, args);
		}

		public void Close(LinkData data)
		{
			data.manager.Destroy(data);
		}

		public void Destroy(LinkData data)
		{
			data.presenter.Dispose();
		}
    }
}