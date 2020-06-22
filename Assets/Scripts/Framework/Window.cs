using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.Framework.Core;
using MVP.Framework.Windows;
using UniRx;

namespace MVP.Framework
{
    public class Window : ILinkDataManager
    {
        public static   Window instance     { get; private set; }
        private         Factory             factory;
        private         List<LinkData>      list; 

        public static async Task Setup()
        {
            var factory = await Factory.Create();
            instance = new Window(factory);
        }

        private Window(Factory factory)
        {
            this.factory = factory;
	        this.list = new List<LinkData>();
        }

        public async Task<LinkData> Load(string path, params object[] args)
        {
            return await factory.Load(path, this, args);
        }

        public async void Open(string path)
        {
            await Wait(path);
        }

        public async Task<object[]> Wait(string path, params object[] args)
        {
            var data = await Instantitate(path, args);
            await data.subject.AsObservable();

            return data.result;
        }

        public void Close(LinkData data)
        {
            var container = data.container as Compoents.Windows.Container;
            if (!container.Validate())
            {
                data.subject.OnError(new System.Exception($"{data.path} window not validate"));
                return;
            }

            container.Destroy();
            var subject = data.subject;
            data.subject = null;
            data.presenter.Dispose();

            subject.OnNext(Unit.Default);
            subject.OnCompleted();
            subject.Dispose();
        }

        public void Destroy(LinkData data)
        {
            list.Remove(data);
            if (data.subject != null)
                data.subject.OnError(new System.Exception($"{data.path} window has be destroy"));
        }

        private async Task<LinkData> Instantitate(string path, params object[] args)
        {
            var data = await factory.Instantitate(this, path, args);
            data.subject = new Subject<Unit>();

            list.Add(data);
	        Arrange();

            return data;
        }

	    private void Arrange()
	    {
	        var ret = list.Aggregate<LinkData, int>(0, (context, data) => context + 1);
            // @todo arrange window by style (eg. popup, dialog ..)
	    }
    }
}
