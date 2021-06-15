using System;
using System.Collections.Generic;
using UnityEngine;
using Framework.Core;
using Framework.Views;

namespace Framework.Components.Windows
{
    public interface ITrait
    {
        void Bind(Container container);
        TraitContext Adjust(TraitContext traitContext);
    }

    public class TraitContext
    {
        public bool barrier;
        public bool opaque;
        public bool visible;
    }

    public class Container : Component
    {
        [Inspector] private readonly GameObject content = null;
        [Inspector] private readonly GameObject barrier = null; 
        [Inspector] private readonly GameObject animation = null;

        private LinkData    data;
        private ITrait      trait;

        private static readonly List<Type> TraitPathList = new List<Type>
        {
            typeof(Dialog), typeof(Activity), typeof(Floater),
        };

        public void Bind(object manager, LinkData linkData)
        {
            data                        = linkData;
            data.container              = this;
            trait                       = GetTrait(linkData.node);

            trait.Bind(this);
            data.node.transform.SetParent(content.transform, false);

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
            UnityEngine.Object.Destroy(context.gameObject);
        }

        public void PlayAnimation(string name)
        {
            var animationComp = animation.GetComponent<Animation>();
            animationComp.Play(name);
        }

        public TraitContext Adjust(TraitContext traitContext)
        {
            var adjust= trait.Adjust(traitContext);
            barrier.SetActive(adjust.barrier);
            context.gameObject.SetActive(traitContext.visible);
            traitContext.visible = traitContext.visible && !adjust.opaque;
            return traitContext;
        }

        protected override void Dispose()
        {
            data.manager.Destroy(data);
            this.data = null;
        }

        private ITrait GetTrait(GameObject node)
        {
            foreach (var type in TraitPathList)
            {
                var component = GetPeer(node, type);
                if (component != null) return component as ITrait;
            }

            Log.Framework.Exception(new Exception($"Can not find trait in {node.gameObject.name}"));
            return null;
        }
    }
}
