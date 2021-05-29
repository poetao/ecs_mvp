using System;
using Framework;
using Framework.Core;
using Framework.Core.States;
using Framework.Features;
using UniRx;

namespace Game.Features
{
    public class BANK_INFO
    {
        public ulong money;
        public ulong coin;
    }

    [Serializable]
    public class UserData : State 
    {
        public BANK_INFO BankInfo
        {
            get
            {
                return Get<BANK_INFO>("BankInfo");
            }
            set
            {
                Set("BankInfo", value);
            }
        }
    }

    public class User : Feature
    {
        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);

            InitState(state);
        }

        private void InitState(IState state)
        {
            var stateRef = state as Reference;
            if (stateRef == null)
            {
                Vendor.Log.User.E("state is not a valid reference for store");
                return;
            }

            this.state = stateRef.GetState();
            var userData = this.state as UserData;
            if (userData == null)
            {
                Vendor.Log.User.E("state is not a valid UserData");
                return;
            }

            userData.BankInfo = new BANK_INFO() { money = 10, coin = 20 };
        }
    }
}
