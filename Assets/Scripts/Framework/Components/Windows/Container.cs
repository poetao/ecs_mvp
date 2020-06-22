using MVP.Framework.Core;
using UnityEngine;

namespace MVP.Framework.Compoents.Windows
{
    public class Container : MonoBehaviour 
    {
        private LinkData    data;

        public void Bind(object manager, LinkData data)
        {
            this.data                       = data;  
            this.data.container             = this;

            this.data.node.transform.SetParent(transform, false);

            // @todo wait add more animtion effect when switch window
        }

        public LinkData GetTarget()
        {
            return data;
        }

        public bool Validate()
        {
            return true;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            data.manager.Destroy(data);
        }
    }
}
