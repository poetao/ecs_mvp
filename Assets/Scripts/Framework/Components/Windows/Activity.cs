namespace MVP.Framework.Components.Windows
{
    public class Activity : Component, ITrait
    {
        public void Bind(Container container)
        {
        }

        public TraitContext Adjust(TraitContext context)
        {
            return new TraitContext()
            {
                barrier = true,
                opaque  = true,
            };
        }
    }
}