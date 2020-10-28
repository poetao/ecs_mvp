using System;
using System.Collections.Generic;
using MVP.Framework;
using MVP.Framework.Core;
using MVP.Framework.Resources;

namespace Presenters.Framework
{
	using Context = MVP.Framework.Presenters.Context;
	using Builder = MVP.Framework.Bootstraps.Components.Context;

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

		public override void Dispose()
		{
			base.Dispose();
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