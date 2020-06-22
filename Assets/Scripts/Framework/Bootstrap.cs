using UnityEngine;
using UnityEngine.SceneManagement;
using MVP.Framework.Bootstraps;

namespace MVP.Framework
{
    public class Bootstrap : Object
    {
        public static async void Start(IStartupContext context)
        {
            await SceneManager.LoadSceneAsync("BootStrap");
            Bridge.Setup();
            await Bridge.instance.Boot(context);
        }
    }
}
