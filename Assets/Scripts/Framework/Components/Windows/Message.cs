using UnityEngine;
using UniRx;
using UnityEngine.UI;
using MVP.Framework.Views;

namespace MVP.Framework.Components.Windows
{
    public class Message : Component
    {
        [Inspector]
        public GameObject text;

        [Inspector]
        public GameObject btnClose;

        [Inspector]
        public GameObject btnTest;

        public override void Create(Context context)
        {
            base.Create(context);

            SubScrible();
        }

        private void SubScrible()
        {
            this.context.state.GetObservable("text")
                .Select(x => x.RefOf<string>())
                .Subscribe(x => text.GetComponent<Text>().text = x);
            btnClose.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(x => context.presenter.Close());
            btnTest.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(x =>
                {
                    Window.instance.Open("Framework/Windows/Toast");
                });
        }
	}
}
