using System;
using System.Threading.Tasks;
using UniRx;

namespace Vendor.Game
{
    public class Manager : IDisposable
    {
        public static bool Check()
        {
            return true;
        }

        public static async void Play(string name, params object[] args)
        {
            var manager = Build(name);
            var result = await manager.Run(name, args);
            if (!result) manager.Dispose();
        }

        private static Manager Build(string name)
        {
            return new Manager();
        }

        public async Task<bool> Run(string name, params object[] args)
        {
            await Load(name, args);
            return true;
        }

        public void Dispose()
        {
        }

        private async Task Load(string name, params object[] args)
        {
            var loader = Loader.Create(name);
            loader.Startup(args);
            await Observable.TimerFrame(1);
        }
    }
}