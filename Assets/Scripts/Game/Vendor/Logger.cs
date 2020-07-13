using MVP.Framework.Core.Logs;

namespace Vendor
{
    public class Log
    {
        public static Logger System = Logger.Create("System", LEVEL.WARN);
        public static Logger Battle = Logger.Create("Battle", LEVEL.WARN);
    }
}