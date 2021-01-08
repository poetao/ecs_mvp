using System;
using System.Collections.Generic;
using UniRx;
using MVP.Framework;
using MVP.Framework.Presenters;
using Features;
using Presenters.Logins;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Login : Presenter
    {
        private Scene   scene;
        private Window  window;
        private Account account;
        private Framework.List serverList;
        private ServerItem serverItem;

        protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
            scene       = builder.GetManager<Scene>();
            window		= builder.GetManager<Window>();
            account     = builder.GetFeature<Account>();

            subPresenters.Add("server", context.Build("Framework/List"));
            serverList = Refrence("server") as Framework.List;
            subPresenters.Add("item", context.Build("Logins/ServerItem"));
            serverItem = Refrence("item") as ServerItem;
            serverItem.SetData(new ServerItemInfo { name = "S3", id = 3, icon = "icon" });

            Subscrible();
        }

        private void Subscrible()
        {
            var observable = account.GetObservable("");
            observable.Subscribe(x =>
            {
                var data = x.RefValue<AccountData>();
                state.Set("name", data.nickName);
            });
            var serverListInfo = new Framework.LIST_INFO()
            {
                list = new List<ServerItemInfo>
                {
                    new ServerItemInfo { name = "S1", id = 0, icon = "icon", },
                    new ServerItemInfo { name = "S2", id = 1, icon = "icon", },
                    new ServerItemInfo { name = "S3", id = 4, icon = "icon", },
                },
                action = OnRefreshServer,
            };
            serverList.Update(serverListInfo);
        }

        private void OnRefreshServer(Presenter presenter, object info)
        {
            var serverItem = presenter as ServerItem;
            serverItem.SetData(info as ServerItemInfo);
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

        private void DoGC()
        {
            System.GC.Collect();
            System.GC.Collect();
            System.GC.Collect();
            window.Wait("Framework/Windows/Message", "wutao");
        }
    }
}
