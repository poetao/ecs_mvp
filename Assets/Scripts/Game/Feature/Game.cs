using MVP.Framework;
using MVP.Framework.Core.States;
using MVP.Framework.Features;
using UniRx;

namespace Features
{
    using GameManager = Vendor.Game.Manager;
    public class Game : Feature
    {
        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);
        }

        public async void Play(int level)
        {
            GameManager.Play("Game", level);
            await Observable.TimerFrame(1);
        }
    }
}