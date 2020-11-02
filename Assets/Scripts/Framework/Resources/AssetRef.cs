using System;
using System.Collections.Generic;
using System.Threading;
using MVP.Framework.Core;
using UnityEditor.VersionControl;
using UnityEngine;

namespace MVP.Framework.Resources
{
    public class AssetRef
    {
        public string                  path         { get; }
        public int                     count        { get; private set; }
        public long                    time         { get; private set; }
        public List<AssetRef>          dependencies { get; }
        private UnityEngine.Object     asset;

        public AssetRef(string path, UnityEngine.Object asset, List<AssetRef> dependencies = null)
        {
            this.path         = path;
            this.asset        = asset;
            this.dependencies = dependencies ?? new List<AssetRef>();
            this.time         = DateTime.Now.Ticks;
            this.count        = 0;
            foreach (var dependence in this.dependencies) dependence.Refrence();
        }

        public AssetRef Refrence()
        {
            ++count;
            time = DateTime.Now.Ticks;
            return this;
        }

        public void Release()
        {
            --count;
            time = DateTime.Now.Ticks;
        }

        public bool Valid()
        {
            return asset != null;
        }

        public UnityEngine.Object Get()
        {
            return asset;
        }

        public T Get<T>() where T : UnityEngine.Object
        {
            return asset as T;
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

            foreach (var dependence in dependencies) dependence.Release();
            if (asset is AssetBundle)
            {
                (asset as AssetBundle).Unload(true);
            }
            else
            {
                UnityEngine.Object.DestroyImmediate(asset, true);
            }
        }
    }
}