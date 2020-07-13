using MVP.Framework;
using MVP.Framework.Views;

namespace Views
{
    [Link("name")]
    [Link("enableLogin")]
    [Link("enableEnter")]

    [Slot("DoLogin")]
    [Slot("SwitchToHome")]

    public class Login : View
    {
        public bool enableLogin(bool isLogin) { return !isLogin; }
        public bool enableEnter(bool isLogin) { return isLogin; }
    }
}
