using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using MVP.Framework.Bootstraps;
using MVP.Framework.Core;
using MVP.Framework.Core.Reflections;

namespace MVP.Framework.Views
{
    [Serializable]
    public class ProxyParameterArray
    {
        public ProxyParameter[] array;

        public ProxyParameter this[int index]
        {
            get
            {
                return array[index];
            }
        }

        public ProxyParameterArray(int size)
        {
            array = new ProxyParameter[size];
        }
    }

    public class Proxy : MonoBehaviour
    {
        public string           View;
        public string[]         linkProperties;
        public GameObject[]     linkComponents;
        public string[]         slotProperties;
        public GameObject[]     slotComponents;
        public ProxyParameterArray[] slotParameters;
        public float[]          slotThrottles;

        private void Awake()
        {
            if (Bridge.instance == null)
            {
                var sceneName = SceneManager.GetActiveScene().name;
                Log.Framework.W($"Please open {sceneName} scene after Bootstrap!");
                return;
            }

            Bridge.instance.component.Initialization(this, View);
        }

		public void DestroySelf()
	    {
			Destroy(this);
	    }
    }

}
