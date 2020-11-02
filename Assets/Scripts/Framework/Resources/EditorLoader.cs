using System;
using System.Threading.Tasks;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace MVP.Framework.Resources
{
    public class EditorLoader : Resource
    {
        protected override async Task<AssetBundleManifest> LoadAssetBundleManifest()
        {
            await Observable.TimerFrame(1);
            return null;
        }

        public override async Task<AssetRef> LoadAsync(string path, TYPE type, LoadingOption option, string assetBundlePath)
        {
            path = Core.Path.instance.Resolve(path, TYPE.Prefab);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var result = await Loading.instance.Wrap( async () =>
            {
                await Observable.TimerFrame(1);
                option.progress?.Invoke(1.0f);
                return true;
            }, option);
            if (!result) return null;

            var assetRef = new AssetRef(path, asset);
            await Observable.TimerFrame(1);
            return assetRef;
        }
    }
}