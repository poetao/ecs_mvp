using UnityEngine;
using MVP.Framework.Views;
using UniRx;
using UnityEngine.UI;

namespace MVP.Framework.Components.Windows
{
    using Context   = Context;

    public class Message : Component
    {
        [Inspector]
        public GameObject text;

        public override void Create(Context context)
        {
            base.Create(context);

            SubScrible();
        }

        private void SubScrible()
        {
            this.context.state.GetObservable("text")
                .Select(x => x.Value<string>())
                .Subscribe(x => text.GetComponent<Text>().text = x);
        }
	}
}
