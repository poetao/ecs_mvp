using MVP.Framework;
using MVP.Framework.Core.States;
using MVP.Framework.Features;

namespace Features
{
    public class Stage : Feature
    {
        public override void Create(Manager manager, IState state, params object[] args)
        {
            base.Create(manager, state, args);

            store.Set("id", 0);
        }
    }
}
