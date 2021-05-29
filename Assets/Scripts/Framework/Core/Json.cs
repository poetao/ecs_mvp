using System;
using System.Collections;
using Newtonsoft.Json;
using UniRx;

namespace Framework.Core
{
    public static class Json
    {
        public static IObservable<T> FromString<T>(string data)
        {
            return Observable.FromMicroCoroutine<T>(observer =>
            {
                return FromStringByCoroutine(observer, data);
            });
        }

        public static IEnumerator FromStringByCoroutine<T>(IObserver<T> observer, string data)
        {
            yield return null;

            var ret = JsonConvert.DeserializeObject<T>(data);
            observer.OnNext(ret);
            observer.OnCompleted();
        }

        public static IObservable<string> ToString(object data)
        {
            return Observable.FromMicroCoroutine<string>(observer =>
            {
                return ToStringByCoroutine(observer, data);
            });
        }

        public static IEnumerator ToStringByCoroutine(IObserver<string> observer, object data)
        {
            yield return null;

            var ret = JsonConvert.SerializeObject(data);
            observer.OnNext(ret);
            observer.OnCompleted();
        }
    }
}
