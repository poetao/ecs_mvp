using System;
using System.Collections.Generic;
using UniRx;
using Framework;
using Framework.Core.States;
using Framework.Presenters;
using Game.Features;
using Game.Presenters.Logins;

namespace Game.Presenters
{
    using Builder = Framework.Bootstraps.Components.Context;

    public class Login : Presenter
    {
        private Scene   scene;
        private Window  window;
        private Account account;
        private Frameworks.List serverList;

        protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
            scene       = builder.GetManager<Scene>();
            window		= builder.GetManager<Window>();
            account     = builder.GetFeature<Account>();

            subPresenters.Add("server", context.Build("Frameworks/List"));
            serverList = Refrence("server") as Frameworks.List;
            subPresenters.Add("item", context.Build("Logins/ServerItem"));

            Subscrible();
        }

        private void Subscrible()
        {
            account.GetObservable().Subscribe(x =>
            {
                if (x == WrapBase.Empty) return;

                state.Set("name", x.RefOf<AccountData>().NickName);
            });

            var serverListInfo = new Frameworks.LIST_INFO()
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
            serverItem?.SetData(info as ServerItemInfo);
        }

        public void DoLogin(int value, float aa)
        {
            if (account.IsLogin()) account.Logout();

            account.LocalLogin(new AccountData()
            {
               Id = 1,
               NickName = $"TEST_{DateTime.Now.Ticks}",
            });
            state.Set("isLogin", true);
        }

        public async void SwitchToHome()
        {
            await scene.Run("Home");
        }

        public void DoGC()
        {
            GC.Collect();
            GC.Collect();
            GC.Collect();
            window.Wait("Frameworks/Windows/Message", "wutao");
        }
    }
}
