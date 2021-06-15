using System;
using Framework.Views;
using UniRx;

namespace Framework.Components.Windows
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
