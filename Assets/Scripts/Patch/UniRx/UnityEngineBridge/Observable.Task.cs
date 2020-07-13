using System;
using System.Threading.Tasks;

namespace UniRx
{
    public static class ObservableTask
    {
        public static IObservable<T> Create<T> (Task<T> task) {
            return Observable.Create<T>(observer => {
                task.ContinueWith<int>(t => {
                    if (t.IsCanceled || t.IsFaulted) observer.OnError(t.Exception);
                    else observer.OnNext(t.Result);
        
                    observer.OnCompleted();
        
                    return 1;
                });
        
                return Disposable.Empty;
            });
        }    
    }
}