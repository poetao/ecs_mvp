using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Framework.Bootstraps;
using Framework.Core;

namespace Framework.Views
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
        public string                   presenterRef;
        public string                   path;
        public Path.ASSEMBLY_TYPE       assembly;
        public List<ProxyLinkItem>      linkItems;
        public List<ProxySlotItem>      slotItems;
        public List<ProxyParameterItem> parameterItems;

        private void Awake()
        {
            if (Bridge.Instance == null)
            {
                var sceneName = SceneManager.GetActiveScene().name;
                Log.Framework.I($"Please open {sceneName} scene after Bootstrap!");
                return;
            }

            Bridge.Instance.Component.Initialization(this);
        }

		public void DestroySelf()
	    {
			Destroy(this);
	    }
    }

}
