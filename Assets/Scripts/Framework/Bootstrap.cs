using UnityEngine;
using UnityEngine.SceneManagement;
using Framework.Bootstraps;

namespace Framework
{
    public class Bootstrap : Object
    {
        public static async void Start(IStartupContext context)
        {
            await SceneManager.LoadSceneAsync("Bootstrap");
            Bridge.Setup();
            await Bridge.Instance.Boot(context);
        }
    }
}
