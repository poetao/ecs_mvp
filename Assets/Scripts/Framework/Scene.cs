using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using MVP.Framework.Core;
using MVP.Framework.Bootstraps;

namespace MVP.Framework
{
    public struct SceneData
    {
        public string         name;
        public object[]       args;
        public LoadingOption  option;
        public AsyncOperation operation;
    }

    public class Scene
    {
        public static Scene instance { get; private set; }
        private IDisposable disposable;

        public static void Setup()
        {
            instance = new Scene();
        }

        private Scene() {}

        public async Task<Presenter> Run(string name, params object[] args)
        {
            var data = new SceneData()
            {
                name = name,
                args = args,
                option = new LoadingOption() {block = true},
            };

            return await Run(data);
        }

        public async Task<Presenter> Run(SceneData data)
        {
            if (data.operation == null)
                data.operation = await Load(data.name, data.option);
            var presenter = await Start(data);
            return presenter;
        }

        public async Task<AsyncOperation> Load(string name, LoadingOption option)
        {
            option.allowSceneActivation = false;
            return await Resource.instance.LoadScene(name, option);
        }

        private async Task<Presenter> Start(SceneData data)
        {
            Func<Task<bool>> task = async () =>
            {
                if (data.operation == null)
                    data.operation = await Load(data.name, data.option);
                data.operation.allowSceneActivation = true;
                await data.operation;
                if (!data.operation.isDone) throw new Exception("timeout");

                return data.operation.isDone;
            };
            await Loading.instance.Wrap(task, data.option);

            var context         = new LinkData();
            context.path        = data.name;
            context.node        = new GameObject($"{data.name}<Root>");
            context.presenter   = Bridge.instance.component.Link(context, data.args);

            if (disposable != null) disposable.Dispose();
            disposable = context.presenter;

            return context.presenter;
        }
    }
}
