using System;
using MVP.Framework;
using MVP.Framework.Core;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace Features
{
    [Serializable]
    public struct BANK_INFO : IEquatable<BANK_INFO>
    {
        public ulong money;
        public ulong coin;

        public bool Equals(BANK_INFO other)
        {
            return other.money == money && other.coin == coin;
        }
    }

    [Serializable]
    public class UserData : State 
    {
        [StateField] public BANK_INFO BankInfo;
    }

    public class User : Feature
    {
        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);

            state.Set("bank", new BANK_INFO() { money = 10, coin = 2000, });
        }
    }
}
