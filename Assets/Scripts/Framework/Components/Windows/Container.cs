using MVP.Framework.Core;
using MVP.Framework.Views;
using UnityEngine;

namespace MVP.Framework.Components.Windows
{
    public class Container : Component
    {
        [Inspector]
        public GameObject   content;

        private LinkData    data;

        public void Bind(object manager, LinkData data)
        {
            this.data                       = data;  
            this.data.container             = this;

            this.data.node.transform.SetParent(context.gameObject.transform, false);

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
            UnityEngine.GameObject.Destroy(context.gameObject);
        }

        protected override void Dispose()
        {
            data.manager.Destroy(data);
            this.data = null;
        }
    }
}
