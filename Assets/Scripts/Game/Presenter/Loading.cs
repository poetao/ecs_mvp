using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using MVP.Framework;
using MVP.Framework.Presenters;
using UniRx;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Loading : Presenter, ILoader<object>
    {
        private ProgressInfo       now;
        private List<ProgressInfo> list;
        private Func<Task<object>> promise;

        protected override async void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);

            state.Set("percent", 0f);
            var load = args[0] as Func<ILoader<object>, Task<object>>;
            this.promise = async () => { return await load(this); };
            this.list    = new List<ProgressInfo>();
            this.now     = new ProgressInfo();
            await Observable.TimerFrame(1);
        }

        public ProgressDelegate Spawn(ProgressInfo info)
        {
            var addInfo = info ?? new ProgressInfo();
            list.Add(addInfo);
            ProgressDelegate progressDelegate = (current) =>
            {
                addInfo.current = current;
                Update();
            };

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

            now.current = now.total = 0.0f;
	        list.Aggregate(now, (data, it) =>
            {
                data.current += it.current * it.weight;
                data.total   += it.total * it.weight;
                return data;
            });
            var percent = Mathf.Floor(now.Percent() * 100);
            state.Set("percent", percent);
        }
    }
}
