using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVP.Framework.Components.Windows;
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
            var container = data.container as Components.Windows.Container;
            if (!container.Validate())
            {
                data.subject.OnError(new System.Exception($"{data.path} window not validate"));
                return;
            }

            container.Destroy();
        }

        public void Destroy(LinkData data)
        {
            data.Destroy();
            list.Remove(data);
            Arrange();
        }

        private async Task<LinkData> Instantitate(string path, params object[] args)
        {
            var data = await factory.Instantitate(this, path, args);
            data.subject = new Subject<Unit>();

            list.Insert(0, data);
	        Arrange();

            return data;
        }

	    private void Arrange()
        {
            var traitContext = new TraitContext() { visible = true, };
	        list.Aggregate(traitContext, (context, data) =>
            {
                return data.container.Adjust(context);
            });
	    }
    }
}
