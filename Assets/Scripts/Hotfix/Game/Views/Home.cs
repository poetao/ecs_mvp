using Framework;
using Framework.Views;

namespace Game.Views
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
