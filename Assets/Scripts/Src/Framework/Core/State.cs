using System;
using System.Collections.Generic;
using System.Text;
using UniRx;
using Framework.Core.States;

namespace Framework.Core
{
    [AttributeUsage(AttributeTargets.Field)]
    public class StateFieldAttribute : Attribute
    {
        public string path;

        public StateFieldAttribute(string path = "")
        {
            this.path = path;
        }
    }

    [Serializable]
    public class State : IState
    {
        private IDictionary<string, WrapBase> dataSet = new Dictionary<string, WrapBase>();
        private IDictionary<string, ISubject<WrapBase>> subjectMap = new Dictionary<string, ISubject<WrapBase>>();

        public IObservable<WrapBase> GetObservable(string path)
        {
            if (!subjectMap.ContainsKey(path))
            {
                subjectMap[path] = new BehaviorSubject<WrapBase>(WrapBase.Empty);
            }

            return subjectMap[path];
        }

        public WrapBase Get(string name)
        {
            if (dataSet.TryGetValue(name, out var wrap))
            {
                return wrap;
            }

            return WrapBase.Empty;
        }

        public T Get<T>(string name)
        {
            if (dataSet.TryGetValue(name, out var value))
            {
                var wrap = value as Wrap<T>;
                if (wrap != null) return wrap.Value;
            }

            return default(T);
        }

        public void Set<T>(string path, T value)
        {
            Wrap<T> wrap;
            if (dataSet.TryGetValue(path, out var wrapBase))
            {
                wrap = wrapBase as Wrap<T>;
                if (wrap == null)
                {
                    throw new NullReferenceException($"the exist wrap is null for path: {path}");
                }

                OverrideWrap(path, wrap, value);
                return;
            }

            var tuple = SearchSubState(path);
            if (tuple != null)
            {
                wrap = tuple.Item2.Get(tuple.Item1) as Wrap<T>;
                if (wrap != null)
                {
                    OverrideWrap(path, wrap, value);
                    return;
                }
            }

            wrap = WrapPool<T>.Rent();
            wrap.Value = value;
            dataSet.Add(path, wrap);
            if (!subjectMap.ContainsKey(path))
            {
                subjectMap.Add(path, new BehaviorSubject<WrapBase>(WrapBase.Empty));
            }
            NotifyWrap(path, wrap);
        }

        public IDisposable Subscribe(string path, IObserver<WrapBase> observer)
        {
            var subject = GetObservable(path) as BehaviorSubject<WrapBase>;
            var disposable = subject?.Subscribe(observer);
            if (dataSet.ContainsKey(path))
            {
                subject?.OnNext(dataSet[path]);
            }

            return disposable;
        }

        public bool Has(string path)
        {
            return dataSet.ContainsKey(path);
        }

        public void Notify(string path = "")
        {
            if (string.IsNullOrEmpty(path))
            {
                NotifyAll();
                return;
            }

            if (dataSet.TryGetValue(path, out var wrap))
            {
                NotifyWrap(path, wrap);
            }
            else
            {
                NotifyWrap(path, WrapBase.Empty);
            }
        }

        public void Replace(IDictionary<string, WrapBase> dictionary)
        {
            dataSet = dictionary;
        }

        protected bool OverrideWrap<T>(string path, Wrap<T> wrap, T value)
        {
            if (wrap.Value.Equals(value))
            {
                return false;
            }

            wrap.Value = value;
            NotifyWrap(path, wrap);
            return true;
        }

        protected void NotifyWrap(string path, WrapBase wrap)
        {
            if (subjectMap.TryGetValue(path, out var subject))
            {
                subject.OnNext(wrap);
            }

            foreach (var keyValuePair in subjectMap)
            {
                var key = keyValuePair.Key;
                if (!key.StartsWith(path)) continue;
                if (key == path) continue;

                NotifyPath(key);
            }
        }

        protected void NotifyAll()
        {
            foreach (var keyValuePair in subjectMap)
            {
                if (NotifyPath(keyValuePair.Key)) continue;

                Log.State.I("{0} dataSet Do Not Contains Key {1}", this, keyValuePair.Key);
            }
        }

        private bool NotifyPath(string path)
        {
            var subject = subjectMap[path];
            if (dataSet.TryGetValue(path, out var wrap))
            {
                subject.OnNext(wrap);
                return true;
            }

            var tuple = SearchSubState(path);
            if (tuple != null)
            {
                wrap = tuple.Item2.Get(tuple.Item1);
                subject.OnNext(wrap);
                return true;
            }

            return false;
        }

        private Tuple<string, IState> SearchSubState(string path)
        {
            var pathes = path.Split('.');
            var count = pathes.Length;
            for (int i = 0; i < count; ++i)
            {
                var prePath = Concat(pathes, 0, i);
                var postPath = Concat(pathes, i + 1, count - 1);
                if (dataSet.TryGetValue(prePath, out var wrapState))
                {
                    var state = wrapState.RefOf<IState>();
                    if (state == null) continue;

                    return Tuple.Create(postPath, state);
                }
            }

            return null;
        }

        private string Concat(string[] list, int from, int to)
        {
            var count = list.Length;
            if (from > to || from < 0 || to < 0 || from >= count || to >= count)
            {
                return "";
            }

            var stringBuilder = new StringBuilder();
            for (int i = from; i < to + 1; ++i)
            {
                stringBuilder.Append(list[i]);
            }

            return stringBuilder.ToString();
        }
    }
}
