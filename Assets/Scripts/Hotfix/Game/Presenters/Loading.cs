using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Framework;
using Framework.Presenters;
using UniRx;

namespace Game.Presenters
{
    using Builder = Framework.Bootstraps.Components.Context;

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
            list    = new List<ProgressInfo>();
            now     = new ProgressInfo();
            promise = async () =>
            {
                if (load == null) return null;
                return await load(this);
            };

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
            await Task.Delay(500);
            var obj = await promise();
            await Task.Delay(1000);
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
