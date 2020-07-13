using System;
using System.Collections.Generic;
using MVP.Framework;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace Features
{
    [Serializable]
    public class AccountData
    {
        public string         nickName;
        public Int64          id;
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
            var dictionary = new Dictionary<string, Any>() {
                {"Account", new Any(data) },
                {"User", new Any("") },
                {"Stage", new Any("") },
                {"Game", new Any("") },
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

        public void OnLogin(Dictionary<string, Any> data)
        {
            foreach (var info in data)
            {
                var path = $"Features.{info.Key}";
                store.Set(manager.GetFeatureStoreKey(path), info.Value);
                if (info.Key != "Account") manager.Create(path);
            }
            state.Set("isLogin", true);
        }
    }
}