using System.Threading.Tasks;
using MVP.Framework.Core;
using UnityEngine;

namespace MVP.Framework.Resources
{
    public class LocalAssetBundle : Resource
    {
        protected override AssetBundle LoadAssetBundleSingle(string path)
        {
            Log.Resource.I("Local LoadAssetBundleSingle {0}", path);
            return AssetBundle.LoadFromFile(path);
        }

        protected override async Task<AssetBundle> LoadAssetBundleSingleAsync(string path, LoadingOption option, float weight = 1.0f)
        {
            Log.Resource.I("Local LoadAssetBundleSingleAsync {0}", path);
            var createRequest = AssetBundle.LoadFromFileAsync(path);
            var result = await Loading.instance.Wrap(
                async () => { return await HookProgress(createRequest, option.progress, weight); }, option);
            if (!result) return null;

            return createRequest.assetBundle;
        }
    }
}