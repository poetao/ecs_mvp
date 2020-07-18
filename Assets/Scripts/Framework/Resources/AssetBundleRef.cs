using System;
using MVP.Framework.Core;
using UnityEngine;

namespace MVP.Framework.Resources
{
    public class AssetBundleRef
    {
        public string      path        { get; }
        public AssetBundle assetBundle { get; }
        public int         count       { get; set; }
        public long        time        { get; private set; }

        public AssetBundleRef(string path, AssetBundle assetBundle)
        {
            this.path        = path;
            this.assetBundle = assetBundle;
            this.time        = DateTime.Now.Ticks;
            this.count       = 0;
        }

        public void Refrence()
        {
            ++count;
        }

        public void Release()
        {
            --count;
            time = DateTime.Now.Ticks;
        }

        public bool CanDestroy()
        {
            return count <= 0;
        }

        public void Destroy()
        {
            if (count > 0)
            {
                Log.Resource.E("{0} assetbundle has in use when destroy", this.path);
                return;
            }
            assetBundle.Unload(true);
        }
    }
}