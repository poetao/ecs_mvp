using System;
using MVP.Framework.Views;
using UniRx;

namespace MVP.Framework.Components.Windows
{
    public class Toast : Component
    {
        public override void Create(Context context)
        {
            base.Create(context);
            Observable.Timer(TimeSpan.FromMilliseconds(3000))
                .Subscribe(x => context.presenter.Close()).AddTo(context.presenter);
        }
	}
}
