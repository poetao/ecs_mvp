using MVP.Framework.Core.States;
using UnityEngine;

namespace MVP.Framework.Views
{
    public class Context
    {
        public GameObject   gameObject { get; set; }
        public Presenter    presenter   { get; set; }
        public Proxy        proxy       { get; set; }
        public IState       state       { get; set; }
    }
}
