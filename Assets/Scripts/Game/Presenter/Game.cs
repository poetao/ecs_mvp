using MVP.Framework;
using MVP.Framework.Presenters;
using UniRx;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Game : Presenter
    {
        private Window window;
        private Scene  scene;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);
			window = builder.GetManager<Window>();
			scene  = builder.GetManager<Scene>();

			context.state.Set("level", $"GAME SCENE {args[0]}");

			ShowStage();
	    }

		public void DoBack()
		{
			scene.Run("Home");
		}

		public async void ShowStage()
		{
            await Observable.TimerFrame(1);
		}
    }
}
