using System;
using System.Linq;
using System.Collections.Generic;
using UniRx;
using MVP.Framework.Core.States;

namespace MVP.Framework.Core
{
    [Serializable]
    public class State : IState
    {
        private IDictionary<string, Any> Map = new Dictionary<string, Any>();
        private IDictionary<string, ISubject<Any>> SubjectMap = new Dictionary<string, ISubject<Any>>();

        public void Default(string path, Any value)
        {
            if (Map.ContainsKey(path)) return;
            Set(path, value);
        }

        public void Default<T>(string path, T value)
        {
            if (Map.ContainsKey(path)) return;
            Set(path, value);
        }

        public void Set<T>(string path, T value, bool forceNotify = false)
        {
            Set(path, new Any(value), forceNotify);
        }

        public void Set(string path, Any value, bool forceNotify = false)
        {
            var old = this.Map.ContainsKey(path) ? this.Map[path] : Any.Empty;
            this.Map[path] = value;
            if (forceNotify || value != old) this.Notify(path, value);
        }

        public T Get<T>(string path)
        {
            return Get(path).Value<T>();
        }

        public Any Get(string path)
        {
            return this.Map.ContainsKey(path) ? this.Map[path] : default(Any);
        }

        public IObservable<Any> GetObservable(string path)
        {
            if (!this.SubjectMap.ContainsKey(path)) this.SubjectMap[path] = new Subject<Any>();
            return this.SubjectMap[path];
        }

        public IDisposable Subscribe(string path, IObserver<Any> observer)
        {
            var subject = GetObservable(path) as Subject<Any>;
            var disposable = subject.Subscribe(observer);
            if (this.Map.ContainsKey(path)) subject.OnNext(this.Map[path]);
            return disposable;
        }

        public bool Has(string path)
        {
            return Map.ContainsKey(path);
        }

        public void Notify(string path, Any value)
        {
            if (!this.SubjectMap.ContainsKey(path)) this.SubjectMap[path] = new Subject<Any>();
            this.SubjectMap[path].OnNext(value);
        }

        public void Notify()
        {
            foreach (var keyValuePair in this.SubjectMap)
            {
                if (this.Map.ContainsKey(keyValuePair.Key))
                    keyValuePair.Value.OnNext(this.Map[keyValuePair.Key]);
                else
                    keyValuePair.Value.OnNext(Any.Empty);
            }
        }

        public object GetRaw()
        {
            return this.Map;
        }

        public void Replace(IDictionary<string, Any> dictionary)
        {
            this.Map = dictionary;
        }

        private void Notify(string path, bool recursion = true)
        {
            if (this.Map.ContainsKey(path))
            {
                Notify(path, this.Map[path]);
            }
            if (!recursion) return;

            var list = path.Split('.');
            list.Skip(list.Length);
	        list.Aggregate<string, string>("", (ret, data) =>
            {
                ret = $"{ret}.{data}";
                Notify(ret, false);
                return ret;
            });
        }
    }
}
