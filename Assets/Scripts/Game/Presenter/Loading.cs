using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Loading : Presenter, ILoader<object>
    {
        private List<ProgressInfo> list;
        private Func<Task<object>> promise;

        protected override async void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);

            state.Set("percent", 0f);
            var load = args[0] as Func<ILoader<object>, Task<object>>;
            this.promise = async () => { return await load(this); };
            this.list    = new List<ProgressInfo>();
        }

        public ProgressDelegate Spawn(ProgressInfo info)
        {
            info = info ?? new ProgressInfo();
            ProgressDelegate progressDelegate = (ProgressInfo nowInfo) =>
            {
                info = nowInfo;
                Update();
            };
            list.Add(info);

            return progressDelegate;
        }

        public async Task<object> Get()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            var obj = await promise();
            await Task.Delay(TimeSpan.FromSeconds(1));
            return obj;
        }

        private void Update()
        {
            if (list == null || list.Count == 0) return;

	        var now = list.Aggregate((data, it) =>
            {
                data.current += it.current;
                data.total   += it.total;
                return data;
            });
            var isZero = Math.Abs(now.total - 0) < float.Epsilon;
            var percent = ( isZero ? 1 : Mathf.Floor(now.current / now.total)) * 100;
            state.Set("percent", percent);
        }
    }
}
