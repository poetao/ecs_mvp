using MVP.Framework;
using MVP.Framework.Presenters;

namespace Presenters
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Game : Presenter
    {
        private Window window;
        private Scene  scene;
        private int    level;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);
			window = builder.GetManager<Window>();
			scene  = builder.GetManager<Scene>();

			this.level = (int)args[0];

			ShowStage();
	    }

		public async void ShowStage()
		{
			await window.Wait($"Stages/Level{level}");
			scene.Run("Home");
		}
    }
}
