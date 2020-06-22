using System.Threading.Tasks;

namespace MVP.Framework.Bootstraps
{
    public class Bridge
    {
        public static   Bridge      instance    { get; private set; }

        public          Component   component   { get; private set; }
        public          Root        root        { get; private set; }

        public static void Setup()
        {
            instance = new Bridge(new Component());
        }

        private Bridge(Component component)
        {
            this.component = component;
        }

        public async Task Boot(IStartupContext context)
        {
            root = await Root.Create(context);
        }
    }
}
