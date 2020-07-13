using MVP.Framework;
using MVP.Framework.Views;

namespace Views.Stages
{
    [Link("cupboard")]
    [Link("answer")]
    [Link("answerAnimation")]
    [Link("coin")]
    [Link("money")]

    [Slot("ClickCupboard", 1)]
    [Slot("DoPlus")]
    [Slot("DoMinus")]
    [Slot("DoAnswer")]
    [Slot("Close")]

    public class Level1 : View {}
}
