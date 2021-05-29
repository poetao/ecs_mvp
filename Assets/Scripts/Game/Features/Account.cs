using System;
using System.Collections.Generic;
using Framework;
using Framework.Core;
using Framework.Core.States;
using Framework.Features;

namespace Game.Features
{
    public class AccountData : State
    {
        public string NickName
        {
            get
            {
                return Get<string>("NickName");
            }
            set
            {
                Set("NickName", value);
            }
        }

        public Int64 Id
        {
            get
            {
                return Get<Int64>("Id");
            }
            set
            {
                Set("Id", value);
            }
        }
    }

    public class Account : Feature
    {
        private Manager manager;

        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);
            this.manager = manager;
        }

        public void LocalLogin(AccountData data)
        {
            var dictionary = new Dictionary<string, State>() {
                {"Account", data }, 
                {"User", new UserData() },
                {"Game", new State() },
            };
            OnLogin(dictionary);
        }

        public void Logout()
        {
            state.Set("isLogin", false);
            manager.ClearFeatures(this);
        }

        public bool IsLogin()
        {
            return state.Get<bool>("isLogin");
        }

        public void OnLogin(Dictionary<string, State> data)
        {
            foreach (var info in data)
            {
                var path = $"Game.Features.{info.Key}";
                store.Set(manager.GetFeatureStoreKey(path), info.Value);
                if (info.Key != "Account") manager.Create(path);
            }
            state.Set("isLogin", true);
        }
    }
}
