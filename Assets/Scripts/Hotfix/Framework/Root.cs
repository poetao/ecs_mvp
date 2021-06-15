using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Core;
using UnityEngine;
using Framework.Features;

namespace Framework
{
    public interface IStartupContext
    {
        string                       Setup();
        Dictionary<string, string>   GetModules();
    }

    public class Root
    {
        public static Root instance { get; private set; }
        public Camera UICamera { get; private set; }
        public GameObject ViewPort { get; private set; }

        public static async Task<Root> Create(IStartupContext context)
        {
            instance = new Root();
            await instance.Initialize(context);
            return instance;
        }

        private async Task Initialize(IStartupContext context)
        {
            await Setup();
            await Run(context);
        }

        private async Task Setup()
        {
            Path.Setup();
            Loading.Setup();
            Store.Setup();
            await Resource.Setup();
            Scene.Setup();
            await Window.Setup();
            Storage.Setup();
            Manager.Setup();

            // @todo database, store, netwrok, service, platform, audio ...
        }

        private async Task Run(IStartupContext context)
        {
            var root = await Window.instance.Load("Frameworks/Root");
            ViewPort = root.node.transform.Find("Viewport").gameObject;
            UICamera = root.node.transform.Find("UICamera").GetComponent<Camera>();
            Object.DontDestroyOnLoad(root.node);
            await Scene.instance.Run(context.Setup());
        }
    }
}
