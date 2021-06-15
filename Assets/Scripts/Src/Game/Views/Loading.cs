using Framework;
using Framework.Views;

namespace Game.Views
{
    [Link("ProgressValue")]
    [Link("ProgressText")]

    public class Loading : View
    {
        public float ProgressValue(float percent)
        {
            return percent / 100f;
        }

        public string ProgressText(float percent)
        {
            return $"{percent}%";
        }
    }
}
