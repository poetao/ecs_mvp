using System.Collections.Generic;
using UnityEngine;
using MVP.Framework.Core;

namespace Bootstraps
{
    public class Startup : MVP.Framework.IStartupContext
    {
        [RuntimeInitializeOnLoadMethod]
        public static void Constructor()
        {
            MVP.Framework.Bootstrap.Start(new Startup());
        }
        
        public Dictionary<string, string> GetModules()
        {
            return new Dictionary<string, string> {
                { "Framework/Service", "Vendor/Service" }
            };
        }

        public string Setup()
        {
            EnableDebug(false);
            SetupManager();

            return "Login";
        }

        private void EnableDebug(bool enable)
        {
        }

        private void SetupManager()
        {
            MVP.Framework.Features.Manager.instance.Create("Features.Account");
        }
    }
}
