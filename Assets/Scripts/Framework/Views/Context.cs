using Framework.Core.States;
using UnityEngine;

namespace Framework.Views
{
    public class Context
    {
        public GameObject   gameObject { get; set; }
        public Presenter    presenter   { get; set; }
        public Proxy        proxy       { get; set; }
        public IState       state       { get; set; }
    }
}
