using System;
using System.Linq;
using System.Collections.Generic;
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

        public T Get<T>(string name)
        {
            if (dataSet.TryGetValue(name, out var value))
            {
                var wrap = value as Wrap<T>;
                if (wrap != null) return wrap.Value;
            }

            return default(T);
        }

        public void Set<T>(string name, T value)
        {
            Wrap<T> wrap;
            if (dataSet.TryGetValue(name, out var existValue))
            {
                wrap = existValue as Wrap<T>;
                if (wrap == null)
                {
                    wrap = WrapPool<T>.Rent();
                    dataSet.Remove(name);
                    dataSet.Add(name, wrap);
                }
                else
                {
                    if (wrap.Value.Equals(value)) return;
                }
            }
            else
            {
                wrap = WrapPool<T>.Rent();
                dataSet.Add(name, wrap);
            }

            wrap.Value = value;
            Notify(name, wrap);
        }

        protected void Notify(string name, WrapBase wrap)
        {
            if (!subjectMap.ContainsKey(name))
            {
                subjectMap[name] = new BehaviorSubject<WrapBase>(WrapBase.Empty);
            }

            subjectMap[name].OnNext(wrap);
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

        public void Notify()
        {
            foreach (var keyValuePair in subjectMap)
            {
                if (!dataSet.ContainsKey(keyValuePair.Key)) continue;
                keyValuePair.Value.OnNext(dataSet[keyValuePair.Key]);
            }
        }

        public IDictionary<string, WrapBase> GetRaw()
        {
            return dataSet;
        }

        public void Replace(IDictionary<string, WrapBase> dictionary)
        {
            dataSet = dictionary;
        }

        private void Notify(string path, bool recursion = true)
        {
            if (dataSet.ContainsKey(path))
            {
                Notify(path, dataSet[path]);
            }
            if (!recursion) return;

            var list = path.Split('.');
            list.Skip(list.Length).Aggregate("", (ret, data) =>
            {
                ret = $"{ret}.{data}";
                Notify(ret, false);
                return ret;
            });
        }
    }
}
