using System;
using System.Collections.Generic;
using UnityEngine;
using MVP.Framework.Core;
using MVP.Framework.Views;
using UniRx.Triggers;

namespace MVP.Framework.Components.Windows
{
    public interface ITrait
    {
        void Bind(Container container);
        TraitContext Adjust(TraitContext context);
    }

    public class TraitContext
    {
        public bool barrier;
        public bool opaque;
        public bool visible;
    }

    public class Container : Component
    {
        [Inspector]
        public GameObject   content;

        [Inspector]
        public GameObject   barrier;

        [Inspector]
        public GameObject   animation;

        private LinkData    data;
        private ITrait      trait;

        private static List<System.Type> traitPathList = new List<System.Type>
        {
            typeof(Dialog), typeof(Activity), typeof(Floater),
        };

        public void Bind(object manager, LinkData data)
        {
            this.data                       = data;  
            this.data.container             = this;
            this.trait                      = GetTrait(data.node);

            this.trait.Bind(this);
            this.data.node.transform.SetParent(content.transform, false);

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
            GameObject.Destroy(context.gameObject);
        }

        public void PlayAnimation(string name)
        {
            var animationComp = animation.GetComponent<Animation>();
            animationComp.Play(name);
        }

        public TraitContext Adjust(TraitContext context)
        {
            var data = trait.Adjust(context);
            barrier.SetActive(data.barrier);
            this.context.gameObject.SetActive(context.visible);
            context.visible = context.visible && !data.opaque;
            return context;
        }

        protected override void Dispose()
        {
            data.manager.Destroy(data);
            this.data = null;
        }

        private ITrait GetTrait(GameObject node)
        {
            foreach (var type in traitPathList)
            {
                var component = GetPeer(node, type);
                if (component != null) return component as ITrait;
            }

            Log.Framework.Exception(new Exception($"Can not find trait in {node.gameObject.name}"));
            return null;
        }
    }
}
