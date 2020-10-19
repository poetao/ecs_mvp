using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using MVP.Framework;
using MVP.Framework.Core;
using MVP.Framework.Resources;

namespace Vendor.Game
{
    using ProgressDelegateSpawn = Func<ProgressInfo, ProgressDelegate>;

    internal class LoaderAssets
    {
        public GameObject                     scene;
        public Dictionary<string, AssetRef>   assets;
    }

    public class Loader
    {
        private List<string>                   resources;
        private Dictionary<string, AssetRef>   assets;

        public static Loader Create(string name)
        {
            return new Loader(Resource.Get(name));
        }

        private Loader(List<string> resources)
        {
            this.resources = resources;
            this.assets    = new Dictionary<string, AssetRef>();
        }

        public async void Startup(params object[] args)
        {
            await Run(args);
        }

        public LinkData Instantiate(string path, params object[] args)
        {
            return Utils.Instantitate(assets[path], path, null, args);
        }

        private async Task<LoaderAssets> Load(ILoader<object> loading, List<string> resources)
        {
            Func<ILoader<object>, LoadingOption> buildOption = (x) =>
            {
                return new LoadingOption() {block = false, progress = x.Spawn()};
            };

            async Task<Tuple<string, AssetRef>> LoadFunc(string path)
            {
                var go = await MVP.Framework.Resource.instance.LoadAsync(path, buildOption(loading));
                return Tuple.Create(path, go);
            };

            var assets = new LoaderAssets();

            var tasks = new List<IObservable<Tuple<string, AssetRef>>>();
            tasks = resources.Aggregate(tasks, (data, it) =>
            {
                data.Add(ObservableTask.Create(LoadFunc(it)));
                return data;
            });

            var promise = new Subject<int>();
            var task = tasks.WhenAll().Subscribe(x =>
            {
                assets.assets = x.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
                promise.OnNext(0);
                promise.OnCompleted();
            });
            await promise;
            promise.Dispose();
            task.Dispose();

            return assets;
        }

        private async Task Run(params object[] args)
        {
            Func<ILoader<object>, Task<object>> load = async (loader) => await Load(loader, resources);
            var loading = (await Scene.instance.Run("Loading", load)) as ILoader<object>;
            var loaderAssets = await loading.Get() as LoaderAssets;
            this.assets = loaderAssets.assets;

            var data = new SceneData()
            {
                name     = "Game",
                args     = args,
                option   = new LoadingOption() { block = false },
            };
            await Scene.instance.Run(data);
        }
    }
}