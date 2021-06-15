using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Framework.Core;
using UnityEngine.SceneManagement;

namespace Framework
{
    public class Bootstrap : MonoBehaviour
    {
        public bool useDebug;
        public bool useILRuntime;

        [RuntimeInitializeOnLoadMethod]
        public static async void OnLoad()
        {
            await SceneManager.LoadSceneAsync("Bootstrap");
        }

        public async void Start()
        {
            var resutl = await LoadAsync();
            if (!resutl)
            {
                Log.Startup.E("Bootstrap Fail!");
            }
        }

        private async Task<bool> LoadAsync()
        {
            byte[] data;
            byte[] symbol = null;
            var path = AutoPatch.ResolveHotfix4Web("Hotfix.dll");
            using (var webRequest = UnityWebRequest.Get(path))
            {
                await webRequest.SendWebRequest();
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    Log.Startup.E($"Load {path} error: {webRequest.error}");
                    return false;
                }

                data = webRequest.downloadHandler.data;
            }

            if (useDebug)
            {
                path = AutoPatch.ResolveHotfix4Web("Hotfix.pdb");
                using (var webRequest = UnityWebRequest.Get(path))
                {
                    await webRequest.SendWebRequest();
                    if (webRequest.isHttpError || webRequest.isNetworkError)
                    {
                        Log.Startup.E($"Load Script.pdb error: {webRequest.error}");
                    }
                    else
                    {
                        symbol = webRequest.downloadHandler.data;
                    }
                }
            }

    #if !ENABLE_IL2CPP
            if (!useILRuntime)
            {
                LoadAssemblyAsDll(data);
                return true;
            }
    #endif

            await LoadAssemblyAsILRuntime(data, symbol);
            return true;
        }

        private void LoadAssemblyAsDll(byte[] data)
        {
            var assembly = Assembly.Load(data);
            // var hotFixTypes = assembly.GetExportedTypes().ToList();

            var type = assembly.GetType("Game.Bootstraps.Startup");
            var method = type.GetMethod("Constructor");
            method?.Invoke(null, null);
        }

        private async Task LoadAssemblyAsILRuntime(byte[] data, byte[] symbol)
        {
            //首先实例化ILRuntime的AppDomain，AppDomain是一个应用程序域，每个AppDomain都是一个独立的沙盒
            var appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();

            var fs = data != null ? new MemoryStream(data) : null;
            var p  = symbol != null ? new MemoryStream(symbol) : null;
            try
            {
                appDomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
            }
            catch (Exception e)
            {
                Log.Startup.E(e);
                return;
            }

            Vendors.ILRuntimes.Loader.InitializeILRuntime(appDomain);
            // var hotFixTypes = appDomain.LoadedTypes.Values.Select(x => x.ReflectionType).ToList();

            if (useDebug)
            {
                // 启动调试服务器
                appDomain.DebugService.StartDebugService(56000);

                while (true)
                {
                    if (appDomain.DebugService.IsDebuggerAttached) break;
                    await Task.Delay(1000);
                }
                // 等待一秒，等待调试信息注入完毕
                await Task.Delay(1000);
            }

            Vendors.ILRuntimes.Loader.OnHotFixLoaded(appDomain);
        }
    }
}
