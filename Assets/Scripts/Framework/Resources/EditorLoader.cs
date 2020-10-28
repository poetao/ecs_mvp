using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace MVP.Framework.Resources
{
    public class EditorLoader : Resource
    {
        protected override async Task Create()
        {
        }

        public override async Task<AssetRef> LoadAsync(string path)
        {
            path = Core.Path.instance.Resolve(path, TYPE.Prefab);
            var asset = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            var assetRef = new AssetRef(path, asset);
            return assetRef;
        }
    }
}