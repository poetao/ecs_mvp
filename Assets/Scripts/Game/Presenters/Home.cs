using UniRx;
using Framework;
using Framework.Presenters;
using Game.Features;

namespace Game.Presenters
{
	using Builder = Framework.Bootstraps.Components.Context;
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
			user.GetObservable<BANK_INFO>("BankInfo").Subscribe(x =>
			{
				state.Set("coin", x.coin);
				state.Set("money", x.money);
			}).AddTo(this);
		}

		public async void DoBack()
		{
			await scene.Run("Login");
		}

        public void DoPlay(int level)
        {
	        game.Play(level);
        }
    }
}
