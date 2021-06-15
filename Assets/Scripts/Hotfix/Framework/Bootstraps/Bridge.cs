using System.Threading.Tasks;

namespace Framework.Bootstraps
{
    public class Bridge
    {
        public static   Bridge      Instance    { get; private set; }

        public          Component   Component   { get; }

        public static void Setup()
        {
            Instance = new Bridge(new Component());
        }

        private Bridge(Component component)
        {
            Component = component;
        }

        public async Task Boot(IStartupContext context)
        {
            await Root.Create(context);
        }
    }
}
