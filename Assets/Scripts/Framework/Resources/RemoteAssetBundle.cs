using System.IO;
using System.Threading.Tasks;
using Framework.Core;
using UnityEngine;

namespace Framework.Resources
{
    public class RemoteAssetBundle : Resource
    {
        protected override AssetBundle LoadAssetBundleSingle(string path)
        {
            Log.Resource.I("Remote LoadAssetBundleSingle {0}", path);
            return AssetBundle.LoadFromFile(path);
        }

        protected override async Task<AssetBundle> LoadAssetBundleSingleAsync(string path, LoadingOption option, float weight = 1.0f)
        {
            Log.Resource.I("Remote LoadAssetBundleSingleAsync {0}", path);
            var createRequest = AssetBundle.LoadFromFileAsync(path);
            var result = await Loading.instance.Wrap(
                async () => { return await HookProgress(createRequest, option.progress, weight); }, option);
            if (!result) return null;

            return createRequest.assetBundle;
        }
    }
}