using UniRx;
using MVP.Framework;
using MVP.Framework.Presenters;
using Features;

namespace Presenters.Stages
{
    using Builder = MVP.Framework.Bootstraps.Components.Context;

    public class Level1 : Presenter
    {
        private User   user;

        protected override void Create(Context context, Builder builder, params object[] args)
        {
            base.Create(context, builder, args);
            user   = builder.GetFeature<User>();

            Subscrible();
        }

        private void Subscrible()
        {
            state.Set("right", 7);
            user.GetObservable("bank")
                .Select(x => x.Value<BANK_INFO>())
                .Subscribe(x =>
                {
                    state.Set("coin", x.coin);
                    state.Set("money", x.money);
                }).AddTo(this);
            user.Notify();
        }

        private void WrongAnswer()
        {
            state.Set("answerAnimation", "wrong", true);
        }

        private void RightAnswer()
        {
            Close();
        }

        public void ClickCupboard()
        {
            var key = "cupboard";
            var cupboard = state.Get<string>(key);
            cupboard = (string.IsNullOrEmpty(cupboard) || cupboard.Equals("close")) ? "open" : "close";
            state.Set(key, cupboard);
        }

        public void DoPlus()
        {
            var key = "answer";
            var answer = state.Get<int>(key);
            state.Set(key, answer + 1);
        }

        public void DoMinus()
        {
            var key = "answer";
            var answer = state.Get<int>(key);
            if (answer <= 0) return;

            state.Set(key, answer - 1);
        }

        public void DoAnswer()
        {
            var answer = state.Get<int>("answer");
            var right = state.Get<int>("right");
            if (right != answer)
            {
                WrongAnswer();
                return;
            }

            RightAnswer();
        }

        public void Close()
        {
            context.Close();
        }
    }
}
