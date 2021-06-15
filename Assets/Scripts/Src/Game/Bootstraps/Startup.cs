using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Game.Bootstraps
{
    public class Startup : Framework.IStartupContext
    {
        public static async void Constructor()
        {
            await SceneManager.LoadSceneAsync("Bootstrap");
            Framework.Bootstraps.Bridge.Setup();
            await Framework.Bootstraps.Bridge.Instance.Boot(new Startup());
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
            SetupManager();

            return "Login";
        }

        private void SetupManager()
        {
            Framework.Features.Manager.instance.Create("Game.Features.Account");
        }
    }
}
