using Framework.Views;

namespace Framework.Components.Windows
{
    public class Dialog : Component, ITrait
    {
        [Inspector] private bool animation = false;

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
                barrier = true,
                opaque = false,
            };
        }
    }
}