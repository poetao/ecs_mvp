using Framework;
using Framework.Core.States;
using Framework.Features;
using UniRx;

namespace Game.Features
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