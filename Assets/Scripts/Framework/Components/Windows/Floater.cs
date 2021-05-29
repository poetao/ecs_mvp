using Framework.Views;

namespace Framework.Components.Windows
{
    public class Floater : Component, ITrait
    {
        [Inspector]
        public bool animation;

        private Container container;

        public void Bind(Container container)
        {
            this.container = container;
            if (animation) this.container.PlayAnimation("open");
        }

        public TraitContext Adjust(TraitContext traitContext)
        {
            return new TraitContext()
            {
                barrier = false,
                opaque = false,
            };
        }
    }
}