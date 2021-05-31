using Framework.Core.States;

namespace Framework
{
    public class Store
    {
        public static Store instance { get; private set; }

        private Core.State state; 
        
        public static void Setup()
        {
            instance = new Store();
        }

        private Store()
        {
            state = new Core.State();
        }

        public void Set(string path, IState value)
        {
            state.Set(path, value);
        }

        public void Notify()
        {
            state.Notify();
        }

        public Reference Reference(string path)
        {
            return new Reference(state, path);
        }
    }
}
