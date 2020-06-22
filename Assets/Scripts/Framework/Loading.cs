using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UniRx;
using MVP.Framework.Core;
using MVP.Framework.Loadings;
using UnityEditorInternal.Profiling.Memory.Experimental;

namespace MVP.Framework
{
    internal class LoadingTimeoutError : Exception {}

    internal struct LoadingInfo
    {
        public int                              runningCount;
        public long                             retryTime;
        public List<Subject<bool>>              retryList;
    }

    public class ProgressInfo
    {
        public float current;
        public float total;
    }

    public delegate void ProgressDelegate(ProgressInfo progress);
    public struct LoadingOption
    {
        public bool             block;
        public bool             allowSceneActivation; 
        public ProgressDelegate progress;
    }

    public interface ILoader<T>
    {
        ProgressDelegate Spawn(ProgressInfo info = null);
        Task<T> Get();
    }

    public class Loading
    {
        public static Loading instance { get; private set; }
        public IView view { get; set; }

        private LoadingInfo data;

        public static void Setup()
        {
            instance = new Loading(new Loadings.View());
        }

        private Loading(IView view)
        {
            this.view = view;
            this.data = new LoadingInfo();
            this.data.retryList = new List<Subject<bool>>();
        }

        public async Task<T> Run<T>(Func<Task<T>> asynFunc, LoadingOption option)
        {
            async Task<T> Run()
            {
                var time = DateTime.Now.Ticks;
                try
                {
                    return await asynFunc();
                }
                catch (Exception e)
                {
                    if (!(e is LoadingTimeoutError)) throw e;
                    var retry = await Retry(time);
                    if (retry) return await Run();
                    return default(T);
                }
            }

            try
            {
                if (option.block) Show();
                return await Run();
            }
            finally
            {
                if (option.block) Hide();
            }
        }

        public async Task<T> Wrap<T>(Func<Task<T>> asynFunc, LoadingOption option)
        {
            async Task<T> Func()
            {
                try
                {
                    var ret = await asynFunc();
                    return ret;
                }
                catch (Exception e)
                {
                    Log.Framework.Error(e);
                    throw new LoadingTimeoutError();
                }
            }

            return await Run<T>(Func, option);
        }

        private void Show()
        {
           if (data.runningCount++ == 0) view.Show(); 
        }

        private void Hide()
        {
           if (--data.runningCount == 0) view.Hide(); 
        }

        private async Task<bool> Retry(long time)
        {
            if (time < data.retryTime) return true; 

            if (data.retryList.Count == 0)
            {
                view.Retry((x)=>
                {
                    var list = data.retryList;
                    data.retryList = new List<Subject<bool>>();
                    data.retryTime = DateTime.Now.Ticks;
                    foreach (var subject in list)
                    {
                        subject.OnNext(x);
                        subject.OnCompleted();
                    }
                });
            }

            var promise = new Subject<bool>();
            data.retryList.Add(promise);
            var retry = await promise;
            promise.Dispose();

            return retry;
        }
    }
}