using MVP.Framework;
using MVP.Framework.Views;

namespace Views
{
    [Link("Coin")]
    [Link("Diamon")]

    [Slot("DoBack")]
    [Slot("DoPlay")]

    public class Home : View
    {
        public string Coin(ulong coin) { return $"C: {coin}"; }
        public string Diamon(ulong money) { return $"G: {money}"; }
    }
}
