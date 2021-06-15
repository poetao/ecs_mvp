using Framework;
using Framework.Views;

namespace Game.Views
{
    [Link("NickName")]
    [Link("enableLogin")]
    [Link("enableEnter")]

    [Slot("DoLogin")]
    [Slot("SwitchToHome")]
    [Slot("DoGC")]

    public class Login : View
    {
        public bool enableLogin(bool isLogin) { return !isLogin; }
        public bool enableEnter(bool isLogin) { return isLogin; }
    }
}
