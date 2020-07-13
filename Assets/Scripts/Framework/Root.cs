using System.Collections.Generic;
using System.Threading.Tasks;
using MVP.Framework.Core;
using UnityEngine;
using MVP.Framework.Features;

namespace MVP.Framework
{
    public interface IStartupContext
    {
        string                       Setup();
        Dictionary<string, string>   GetModules();
    }

    public class Root
    {
        public static Root instance { get; private set; }

        public static async Task<Root> Create(IStartupContext context)
        {
            instance = new Root();
            await instance.Initialize(context);
            return instance;
        }

        private async Task Initialize(IStartupContext context)
        {
            await Setup(context);
            await Run(context);
        }

        private async Task Setup(IStartupContext context)
        {
            var maping = context.GetModules();

            Path.Setup();
            Loading.Setup();
            Store.Setup();
            Resource.Setup();
            Scene.Setup();
            await Window.Setup();
            Storage.Setup();
            Manager.Setup();

            // @todo database, store, netwrok, service, platform, audio ...
        }

        private async Task Run(IStartupContext context)
        {
            var root = await Window.instance.Load("Framework/Root");
            GameObject.DontDestroyOnLoad(root.node);
            await Scene.instance.Run(context.Setup());
        }
    }
}