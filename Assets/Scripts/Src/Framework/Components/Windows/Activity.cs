namespace Framework.Components.Windows
{
    public class Activity : Component, ITrait
    {
        public void Bind(Container container)
        {
        }

        public TraitContext Adjust(TraitContext traitContext)
        {
            return new TraitContext()
            {
                barrier = true,
                opaque  = true,
            };
        }
    }
}