using MVP.Framework.Core.States;

namespace MVP.Framework.Views
{
    public class Context
    {
        public Presenter    presenter   { get; set; }
        public Proxy        proxy       { get; set; }
        public IState       state       { get; set; }
    }
}
