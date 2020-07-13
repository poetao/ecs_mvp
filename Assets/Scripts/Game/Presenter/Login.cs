using System;
using UniRx;
using MVP.Framework;
using MVP.Framework.Presenters;
using Features;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Login : Presenter
    {
        private Scene   scene;
        private Window  window;
        private Account account;

        protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
            scene       = builder.GetManager<Scene>();
            window		= builder.GetManager<Window>();
            account     = builder.GetFeature<Account>();

            Subscrible();
        }

        private void Subscrible()
        {
            var observable = account.GetObservable("");
            observable.Subscribe(x =>
            {
                var data = x.Value<AccountData>();
                state.Set("name", data.nickName);
            });
        }

        private void DoLogin()
        {
            if (account.IsLogin()) account.Logout();

            var data = new AccountData();
            data.id = 1;
            data.nickName = $"TEST_{DateTime.Now.Ticks}";
            account.LocalLogin(data);
            state.Set("isLogin", true);
        }

        private async void SwitchToHome()
        {
            await scene.Run("Home");
        }
    }
}
