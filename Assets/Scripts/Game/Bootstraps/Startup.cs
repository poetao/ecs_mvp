using System.Collections.Generic;
using UnityEngine;
using Framework.Core;

namespace Game.Bootstraps
{
    public class Startup : Framework.IStartupContext
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Constructor()
        {
            Framework.Bootstrap.Start(new Startup());
        }
        
        public Dictionary<string, string> GetModules()
        {
            return new Dictionary<string, string> {
                { "Framework/Service", "Vendor/Service" }
            };
        }

        public string Setup()
        {
            RegisterAction.Init();
            EnableDebug(false);
            SetupManager();

            return "Login";
        }

        private void EnableDebug(bool enable)
        {
        }

        private void SetupManager()
        {
            Framework.Features.Manager.instance.Create("Game.Features.Account");
        }
    }
}
