using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MVP.Framework.Bootstraps;
using MVP.Framework.Core;
using MVP.Framework.Core.Reflections;

namespace MVP.Framework.Views
{
    [Serializable]
    public class ProxyLinkItem
    {
        public string name;
        public GameObject gameObject;
        public String componentType;
    }

    [Serializable]
    public class ProxySlotItem
    {
        public string name;
        public GameObject gameObject;
        public float throttle;
        public List<InspectorParameter> parameters;
    }

    [Serializable]
    public class ProxyParameterItem
    {
        public string name;
        public InspectorParameter parameter;
    }

    public class Proxy : MonoBehaviour
    {
        public bool                     isComponent;
        public string                   assembly;
        public string                   path;
        public string                   presenterRef;
        public List<ProxyLinkItem>      linkItems;
        public List<ProxySlotItem>      slotItems;
        public List<ProxyParameterItem> parameterItems;

        private void Awake()
        {
            if (Bridge.instance == null)
            {
                var sceneName = SceneManager.GetActiveScene().name;
                Log.Framework.W($"Please open {sceneName} scene after Bootstrap!");
                return;
            }

            Bridge.instance.component.Initialization(this);
        }

		public void DestroySelf()
	    {
			Destroy(this);
	    }
    }

}
