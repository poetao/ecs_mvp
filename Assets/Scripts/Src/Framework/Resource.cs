using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UniRx.Triggers;
using Framework.Resources;

namespace Framework
{
    using Log = Core.Log;

    public abstract class Resource
    {
        public enum TYPE { Prefab, Scene, AssetBundle, Presenter, View, Component, Storage, Binary }

        public static Resource instance { get; private set; }

        protected Dictionary<int, AssetRef>              assetMap;
        protected Dictionary<string, int>                pathMap;
        protected AssetBundleManifest                    assetBundleManifest;
        private   long                                   sweepTime;

        public static async Task Setup()
        {
#if UNITY_EDITOR
            instance = new EditorLoader();
#elif DEBUG
            instance = new LocalAssetBundle();
#else
            instance = new RemoteAssetBundle();
#endif
            await instance.Create();
        }

        public GameObject Instantiate(AssetRef assetRef)
        {
            assetRef.Refrence();
            var clone = UnityEngine.Object.Instantiate(assetRef.Get<GameObject>());
            clone.OnDestroyAsObservable().Subscribe(_ => assetRef.Release());
            return clone;
        }

        public async void LoadScene(string path)
        {
            SceneManager.LoadScene(path);
            await Observable.TimerFrame(1);
        }

        public async Task<AsyncOperation> LoadSceneAsync(string path, LoadingOption option)
        {
            path = Core.Path.instance.Resolve(path, TYPE.Scene);
            var operation = SceneManager.LoadSceneAsync(path);
            operation.allowSceneActivation = option.allowSceneActivation;
            var result = await Loading.instance.Wrap(async () =>
            {
                return await HookProgress(operation, option.progress);
            }, option);
            if (!result) return null;

            return operation;
        }

        public AssetRef Load(string path, TYPE type = TYPE.Prefab, string assetBundlePath = null)
        {
            path = Core.Path.instance.Resolve(path, type);
            var name = System.IO.Path.GetFileNameWithoutExtension(path);
            var assetRef = FindAssetRef(path);
            if (assetRef != null)
            {
                Log.Resource.I("Load {0} in Cache", name);
                return assetRef.Refrence();
            }

            if (string.IsNullOrEmpty(assetBundlePath))
                assetBundlePath = Core.Path.instance.Resolve(path, TYPE.AssetBundle);
            var assetBundleRef = LoadAssetBundle(assetBundlePath);
            if (assetBundleRef == null) return null;

            var assetBundle = assetBundleRef.Get<AssetBundle>();
            var asset = assetBundle.LoadAsset(name);
            assetRef = new AssetRef(path, asset, new List<AssetRef>() { assetBundleRef });
            CacheAssetRef(assetRef.Refrence());
            return assetRef;
        }

        public async Task<AssetRef> LoadAsync(string path)
        {
            var option = new LoadingOption() { block = true, allowSceneActivation = false, };
            return await LoadAsync(path, TYPE.Prefab, option, "");
        }

        public async Task<AssetRef> LoadAsync(string path, TYPE type)
        {
            var option = new LoadingOption() { block = true, allowSceneActivation = false, };
            return await LoadAsync(path, type, option, "");
        }

        public async Task<AssetRef> LoadAsync(string path, LoadingOption option, TYPE type = TYPE.Prefab)
        {
            return await LoadAsync(path, type, option, "");
        }

        public virtual async Task<AssetRef> LoadAsync(string path, TYPE type, LoadingOption option, string assetBundlePath)
        {
            path = Core.Path.instance.Resolve(path, type);
            var name = System.IO.Path.GetFileNameWithoutExtension(path);
            var assetRef = FindAssetRef(path);
            if (assetRef != null)
            {
                Log.Resource.I("LoadAsync {0} in Cache", name);
                return assetRef.Refrence();
            }

            if (string.IsNullOrEmpty(assetBundlePath))
                assetBundlePath = Core.Path.instance.Resolve(path, TYPE.AssetBundle);
            var assetBundleRef = await LoadAssetBundleAsync(assetBundlePath, option);
            if (assetBundleRef == null) return null;

            var assetBundle = assetBundleRef.Get<AssetBundle>();
            var assetBundleRequest = assetBundle.LoadAssetAsync(name);
            var result = await Loading.instance.Wrap(async () =>
            {
                return await HookProgress(assetBundleRequest, option.progress, 0.7f);
            }, option);
            if (!result) return null;

            var asset = assetBundleRequest.asset;
            if (asset == null) return null;

            assetRef = new AssetRef(path, asset, new List<AssetRef>() { assetBundleRef });
            CacheAssetRef(assetRef.Refrence());
            return assetRef;
        }

        public void Release(AssetRef assetRef)
        {
            assetRef.Release();
        }

        protected async Task Create()
        {
            pathMap             = new Dictionary<string, int>();
            assetMap            = new Dictionary<int, AssetRef>();
            assetBundleManifest = await LoadAssetBundleManifest();
            Observable.Interval(TimeSpan.FromSeconds(3)).Subscribe(_ => Sweep());
        }

        protected virtual async Task<AssetBundleManifest> LoadAssetBundleManifest()
        {
            var option = new LoadingOption();
            var path= $"Assets/{Core.Path.AssetBundleFolder}/{Core.Path.AssetBundleFolder}";
            var assetBundle = await LoadAssetBundleSingleAsync(path, option);
            var assetBundleRef = new AssetRef(path, assetBundle);

            var name = "AssetBundleManifest";
            var manifest = await assetBundle.LoadAssetAsync<AssetBundleManifest>(name);
            var manifestRef = new AssetRef(name, manifest, new List<AssetRef>() { assetBundleRef });
            CacheAssetRef(manifestRef.Refrence());
            return manifest as AssetBundleManifest;
        }

        protected AssetRef LoadAssetBundle(string path)
        {
            var assetBundleRef = FindAssetRef(path);
            if (assetBundleRef != null)
            {
                Log.Resource.I("LoadAssetBundle {0} in Cache", path);
                return assetBundleRef;
            }

            Log.Resource.I("LoadAssetBundle {0}", path);
            return LoadAssetBundleAndDepencencies(path);
        }

        protected AssetRef LoadAssetBundleAndDepencencies(string path, Dictionary<string, bool> wait = null)
        {
            wait = wait ?? new Dictionary<string, bool>();
            if (wait.ContainsKey(path)) return null;

            var dependencies = assetBundleManifest.GetAllDependencies(path);
            foreach (var dependence in dependencies)
            {
                LoadAssetBundleAndDepencencies(dependence, wait);
            }

            var assetBundle = LoadAssetBundleSingle(path);
            var dependenceRefs = dependencies.Select(x => FindAssetRef(x)).ToList();
            var assetBundleRef = new AssetRef(path, assetBundle, dependenceRefs);
            CacheAssetRef(assetBundleRef);
            return assetBundleRef;
        }

        protected async Task<AssetRef> LoadAssetBundleAsync(string path, LoadingOption option)
        {
            var assetBundleRef = FindAssetRef(path);
            if (assetBundleRef != null)
            {
                Log.Resource.I("LoadAssetBundleAsync {0} in Cache", path);
                return assetBundleRef;
            }

            Log.Resource.I("LoadAssetBundleAsync {0}", path);
            return await LoadAssetBundleAndDepencenciesAsync(path, option);
        }

        protected async Task<AssetRef> LoadAssetBundleAndDepencenciesAsync(string path, LoadingOption option, Dictionary<string, bool> wait = null)
        {
            wait = wait ?? new Dictionary<string, bool>();
            if (wait.ContainsKey(path)) return null;

            wait[path] = true;
            var dependencies = assetBundleManifest.GetAllDependencies(path);
            foreach (var dependence in dependencies)
            {
                await LoadAssetBundleAndDepencenciesAsync(dependence, option, wait);
            }

            var assetBundle = await LoadAssetBundleSingleAsync(path, option);
            var dependenceRefs = dependencies.Select(x => FindAssetRef(x)).ToList();
            var assetBundleRef = new AssetRef(path, assetBundle, dependenceRefs);
            CacheAssetRef(assetBundleRef);
            return assetBundleRef;
        }

        protected virtual AssetBundle LoadAssetBundleSingle(string path)
        {
            Log.Resource.Exception(new NotImplementedException());
            return null;
        }

        protected virtual async Task<AssetBundle> LoadAssetBundleSingleAsync(string path, LoadingOption option, float weight = 1.0f)
        {
            Log.Resource.Exception(new NotImplementedException());
            await Observable.TimerFrame(1);
            return null;
        }

        protected void CacheAssetRef(AssetRef assetRef)
        {
            var hashCode = assetRef.Get().GetHashCode();
            if (pathMap.ContainsKey(assetRef.path))
            {
                Log.Resource.E("{0} has already cached in pathMap", assetRef.path);
                return;
            }

            pathMap.Add(assetRef.path, hashCode);
            if (assetMap.ContainsKey(hashCode))
            {
                Log.Resource.E("{0} has already cached in assetMap", assetRef.path);
                return;
            }

            Log.Resource.I("CacheAssetRef {0}", assetRef.path);
            assetMap.Add(hashCode, assetRef);
        }

        protected AssetRef FindAssetRef(string path)
        {
            if (!pathMap.ContainsKey(path)) return null;

            var hashCode = pathMap[path];
            if (!assetMap.ContainsKey(hashCode)) return null;

            return assetMap[hashCode];
        }

        protected void RemoveAssetRef(string path)
        {
            if (!pathMap.ContainsKey(path)) return;

            var hashCode = pathMap[path];
            if (!assetMap.ContainsKey(hashCode)) return;

            Log.Resource.I("RemoveAssetRef {0} : {1}", hashCode.ToString(), path);
            pathMap.Remove(path);
            assetMap.Remove(hashCode);
        }

        protected void Sweep()
        {
            if (this != instance) return;

            var now = DateTime.Now.Ticks;
            if (sweepTime + 60 * 1000 > now) return;

            sweepTime = now;
            lock (assetMap)
            {
                var waitDestroy = from kvPair in assetMap
                    where kvPair.Value.CanDestroy()
                    select kvPair.Value;
                foreach (var assetRef in waitDestroy.ToList())
                {
                    RemoveAssetRef(assetRef.path);
                    assetRef.Destroy();
                }
            }
        }

        protected async Task<bool> HookProgress(AsyncOperation operation, ProgressDelegate progress, float weight = 1.0f )
        {
            var promise = new AsyncSubject<int>();
            var disposable = Observable.Interval(TimeSpan.FromSeconds(1/30f))
                .Select(x => new ProgressInfo()
                {
                    current = x,
                    total = operation.allowSceneActivation ? 1f : 0.9f,
                    weight = weight,
                })
                .TakeWhile(x =>
                {
                    if (x.current < x.total)
                    {
                        return true;
                    }
                    if (progress != null) progress(x.Percent());
                    promise.OnNext(0);
                    promise.OnCompleted();
                    promise.Dispose();
                    return false;
                }).Subscribe(x =>
                {
                    if (progress != null) progress(x.Percent());
                });
            await promise;
            disposable.Dispose();
            return true;
        }
    }
}
