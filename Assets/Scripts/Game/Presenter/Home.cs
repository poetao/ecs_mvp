using UniRx;
using MVP.Framework;
using MVP.Framework.Presenters;
using Features;

namespace Presenters
{
	using Builder = MVP.Framework.Bootstraps.Components.Context;
	using FGame = Features.Game;

    public class Home : Presenter
    {
		private Scene  scene;
		private User   user;
		private FGame  game;

		protected override void Create(Context context, Builder builder, params object[] args)
	    {
			base.Create(context, builder, args);
			scene  = builder.GetManager<Scene>();
			user   = builder.GetFeature<User>();
			game   = builder.GetFeature<FGame>();

			Subscrible();
	    }

		private void Subscrible()
		{
			user.GetObservable("bank")
				.Select(x => x.Value<BANK_INFO>())
				.Subscribe(x =>
			{
				state.Set("coin", x.coin);
				state.Set("money", x.money);
			}).AddTo(this);
			user.Notify();
		}

		public async void DoBack()
		{
			await scene.Run("Login");
		}

        public void DoPlay(int level)
        {
	        this.game.Play(level);
        }
    }
}
