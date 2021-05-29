using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Core;

namespace Framework.Features
{
    public class Manager

    {
        public static Manager instance { get; private set; }

        private Dictionary<Type, object>  managers;
        private Dictionary<Type, Feature> features;
        private Dictionary<Type, object>  store;

        public static void Setup()
        {
            instance = new Manager();
        }

        private Manager()
        {
            managers = new Dictionary<Type, object>()
            {
                { typeof(Scene),       Scene.instance      },
                { typeof(Window),      Window.instance     },
                { typeof(Store),       Store.instance      },
                { typeof(Storage),     Storage.instance    },
                { typeof(Manager),     this                },
            };

            features = new Dictionary<Type, Feature>();
        }

        public void ClearFeatures(Feature exclude)
        {
            var clearFeatures = this.features;
            this.features = new Dictionary<Type, Feature>();
            foreach (var keyValue in clearFeatures)
            {
                if (exclude != keyValue.Value)
                {
                    keyValue.Value.Dispose();
                }
                else
                {
                    this.features.Add(keyValue.Key, keyValue.Value);
                }
            }
            clearFeatures.Clear();
        }

        public void Create(string path, params object[] args)
        {
            var feature = Reflection.CreateInstance<Feature>(path);
            var key = GetFeatureStoreKey(path);
            var state = GetManager<Store>().Reference(key);
            feature.Create(this, state, args);
            features.Add(instance.GetType(), feature);
        }

        public T GetManager<T>() where T : class
        {
            return managers[typeof(T)] as T;
        }

        public T Get<T>() where T : Feature
        {
            return features[typeof(T)] as T;
        }

        public T GetFeature<T>() where T : Feature 
        {
            return Get<T>();
        }

        public void Destroy<T>() where T : Feature
        {
            Get<T>().Dispose();
        }

        public string GetFeatureStoreKey(string path)
        {
            return path.Split('.').Last().ToLowerInvariant();
        }
    }
}
