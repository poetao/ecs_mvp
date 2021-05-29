using Framework.Core.Logs;

namespace Game.Vendor
{
    public class Log
    {
        public static Logger System = Logger.Create("System", LEVEL.WARN);
        public static Logger Battle = Logger.Create("Battle", LEVEL.WARN);
        public static Logger User   = Logger.Create("User",   LEVEL.WARN);
    }
}