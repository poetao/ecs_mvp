using System;
using UniRx;

namespace MVP.Framework.Loadings
{
    public interface IView
    {
        void Show();
        void Hide();
        void Retry(Action<bool> action);
    }

    public class View : IView
    {
        public View()
        {
        }

        public void Show()
        {
        }

        public void Hide()
        {
        }

        public void Retry(Action<bool> action)
        {
            Observable.Return(Unit.Default)
                .Delay(TimeSpan.FromSeconds(1))
                .Subscribe(x => action(true));
        }
    }
}