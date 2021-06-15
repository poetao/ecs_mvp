using Framework;
using UniRx;

namespace Game.Features
{
    using GameManager = Vendor.Game.Manager;
    public class Game : Feature
    {
        public async void Play(int level)
        {
            GameManager.Play("Game", level);
            await Observable.TimerFrame(1);
        }
    }
}