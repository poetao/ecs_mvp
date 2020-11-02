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
    using ASSET_DIC = Dictionary<string, AssetRef>;

    public class Loader
    {
        private string                         name;
        private List<string>                   resources;
        private ASSET_DIC                      assets;

        public static Loader Create(string name)
        {
            return new Loader(name, Resource.Get(name));
        }

        private Loader(string name, List<string> resources)
        {
            this.name      = name;
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

        private async Task<Tuple<ASSET_DIC, AsyncOperation>> Load(ILoader<object> loading, List<string> resources)
        {
            async Task<Tuple<string, object>> LoadAssetsFunc()
            {
                var result = await LoadAssets(loading, resources);
                return Tuple.Create("assets", result as object);
            };

            async Task<Tuple<string, object>> LoadSceneFunc()
            {
                var result = await Scene.instance.Load(this.name, BuildOption(loading));
                return Tuple.Create("scene", result as object);
            };

            var tasks = new List<IObservable<Tuple<string, object>>>();
            tasks.Add(ObservableTask.Create(LoadAssetsFunc()));
            tasks.Add(ObservableTask.Create(LoadSceneFunc()));
            var loaders = await tasks.WhenAll();

            ASSET_DIC assetDic = null; AsyncOperation asyncOperation = null;
            foreach (var loader in loaders)
            {
                if (loader.Item1.Equals("assets")) assetDic = loader.Item2 as ASSET_DIC;
                if (loader.Item1.Equals("scene")) asyncOperation = loader.Item2 as AsyncOperation;
            }

            return Tuple.Create(assetDic, asyncOperation);
        }

        private async Task<ASSET_DIC> LoadAssets(ILoader<object> loading, List<string> resources)
        {
            async Task<Tuple<string, AssetRef>> LoadFunc(string path)
            {
                var go = await MVP.Framework.Resource.instance.LoadAsync(path, BuildOption(loading));
                return Tuple.Create(path, go);
            };

            var tasks = new List<IObservable<Tuple<string, AssetRef>>>();
            tasks = resources.Aggregate(tasks, (data, it) =>
            {
                data.Add(ObservableTask.Create(LoadFunc(it)));
                return data;
            });
            var results = await tasks.WhenAll();

            return results.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);
        }

        private LoadingOption BuildOption(ILoader<object> loading)
        {
            return new LoadingOption() {block = false, progress = loading.Spawn()};
        }

        private async Task Run(params object[] args)
        {
            Func<ILoader<object>, Task<object>> load = async (loader) => await Load(loader, resources);
            var loading = (await Scene.instance.Run("Loading", load)) as ILoader<object>;
            var loaderAssets = await loading.Get() as Tuple<ASSET_DIC, AsyncOperation>;
            this.assets = loaderAssets.Item1;

            var data = new SceneData()
            {
                name      = name,
                args      = args,
                operation = loaderAssets.Item2,
                option    = new LoadingOption() { block = false },
            };
            await Scene.instance.Run(data);
        }
    }
}