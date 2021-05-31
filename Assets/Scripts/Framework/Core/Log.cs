using Framework.Core.Logs;

namespace Framework.Core
{
    public static class Log
    {
        public static Logger Editor         = Logger.Create("Editor",       LEVEL.WARN);
        public static Logger Reflection     = Logger.Create("Reflection",   LEVEL.WARN);
        public static Logger Framework      = Logger.Create("Framework",    LEVEL.WARN);
        public static Logger Resource       = Logger.Create("Resource",     LEVEL.WARN);
        public static Logger State          = Logger.Create("State",        LEVEL.WARN);
        public static Logger Window         = Logger.Create("Window",       LEVEL.WARN);
        public static Logger Startup        = Logger.Create("Startup",      LEVEL.WARN);
    }
}
