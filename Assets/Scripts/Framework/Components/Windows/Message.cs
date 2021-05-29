using UnityEngine;
using UniRx;
using UnityEngine.UI;
using Framework.Views;

namespace Framework.Components.Windows
{
    public class Message : Component
    {
        [Inspector] private readonly GameObject text = null;
        [Inspector] private readonly GameObject btnClose = null;
        [Inspector] private readonly GameObject btnTest = null;

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
                    Window.instance.Open("Frameworks/Windows/Toast");
                });
        }
	}
}
