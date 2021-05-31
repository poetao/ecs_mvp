using Framework.Core;
using Framework.Core.States;

namespace Framework.Presenters
{
    using Builder = Bootstraps.Components.Context;

    public class Context
    {
        public  IState      state { get; private set; }
        private Builder     builder;
        private LinkData    data;

        public static Context Create(Builder builder)
        {
            var state = builder.TakeState() ?? new State();
            return new Context(builder, state);
        }

        private Context(Builder builder, IState state)
        {
            this.state      = state;
            this.builder    = builder;
            this.data       = builder.Data;
        }

        public Presenter Build(string path, params object[] args)
        {
            return builder.Build(path, args);
        }

        public void Close(params object[] args)
        {
            if (data.manager == null) return;

            data.result = args;
            data.manager.Close(data);
        }
    }
}
