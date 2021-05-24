using MVP.Framework.Core.Reflections;

namespace Bootstraps
{
    public static class RegisterAction
    {
        public static void Init()
        {
            MethodDelegateBuilder.RegisteFunc<Views.Login, bool, bool>("enableLogin");
            MethodDelegateBuilder.RegisteFunc<Views.Login, bool, bool>("enableEnter");
            MethodDelegateBuilder.RegisteAction<Presenters.Login>("DoGC");
            MethodDelegateBuilder.RegisteAction<Presenters.Login, System.Int32, System.Single>("DoLogin");
            MethodDelegateBuilder.RegisteAction<Presenters.Login>("SwitchToHome");
            MethodDelegateBuilder.RegisteFunc<Views.Home, System.UInt64, System.String>("Coin");
            MethodDelegateBuilder.RegisteFunc<Views.Home, System.UInt64, System.String>("Diamon");
            MethodDelegateBuilder.RegisteAction<Presenters.Home>("DoBack");
            MethodDelegateBuilder.RegisteAction<Presenters.Home, System.Int32>("DoPlay");
            MethodDelegateBuilder.RegisteFunc<Views.Loading, System.Single, System.Single>("ProgressValue");
            MethodDelegateBuilder.RegisteFunc<Views.Loading, System.Single, System.String>("ProgressText");
            MethodDelegateBuilder.RegisteAction<Presenters.Game>("DoBack");
        }
    }
}