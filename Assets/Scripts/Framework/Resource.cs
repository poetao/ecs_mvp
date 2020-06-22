using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace MVP.Framework
{
    public class Resource
    {
        public enum TYPE { Prefab, Scene, Presenter, View, Storage }

        public static Resource instance { get; private set; }

        public static void Setup()
        {
            instance = new Resource();
        }

        public async Task<AsyncOperation> LoadScene(string path, LoadingOption option)
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

        public T Load<T>(string path, TYPE type = TYPE.Prefab) where T : UnityEngine.Object
        {
            path = Core.Path.instance.Resolve(path, type);
            return Resources.Load<T>(path);
        }

        public async Task<T> LoadAsync<T>(string path, TYPE type = TYPE.Prefab) where T : UnityEngine.Object
        {
            var option = new LoadingOption() { block = true, allowSceneActivation = false, };
            return await LoadAsync<T>(path, type, option);
        }

        public async Task<T> LoadAsync<T>(string path, TYPE type, LoadingOption option) where T : UnityEngine.Object
        {
            path = Core.Path.instance.Resolve(path, type);
            var operation = Resources.LoadAsync<T>(path);
            var result = await Loading.instance.Wrap(async () =>
            {
                return await HookProgress(operation, option.progress);
            }, option);
            if (!result) return default(T);

            return operation.asset as T;
        }

        private async Task<bool> HookProgress(AsyncOperation operation, ProgressDelegate progress)
        {
            var promise = new AsyncSubject<int>();  
            var disposable = Observable.Interval(TimeSpan.FromSeconds(1/30f))
                .Select(x => new ProgressInfo()
                {
                    current = x,
                    total = operation.allowSceneActivation ? 1f : 0.9f,
                })
                .TakeWhile(x =>
                {
                    if (x.current < x.total) return true;
                    promise.OnNext(0);
                    promise.OnCompleted();
                    return false;
                }).Subscribe(x =>
                {
                    if (progress != null) progress(x);
                });
            await promise;
            promise.Dispose();
            disposable.Dispose();
            return true;
        }
    }
}
