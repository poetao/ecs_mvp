using MVP.Framework;
using MVP.Framework.Views;

namespace Views
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
