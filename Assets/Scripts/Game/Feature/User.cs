using System;
using MVP.Framework;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace Features
{
    [Serializable]
    public class BANK_INFO
    {
        public ulong money;
        public ulong coin;
    }

    public class User : Feature
    {
        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);

            state.Default("bank", new BANK_INFO() { money = 0, coin = 2000, });
        }
    }
}