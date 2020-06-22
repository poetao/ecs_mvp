using MVP.Framework.Core.Logs;

namespace MVP.Framework.Core
{
    public static class Log
    {
        public static Logger Editor     = Logger.Create("Editor",       LEVEL.INFO);
        public static Logger Reflection = Logger.Create("Reflection",   LEVEL.WARN);
        public static Logger Framework  = Logger.Create("Framework",    LEVEL.WARN);
        public static Logger State      = Logger.Create("State",        LEVEL.WARN);
        public static Logger Window     = Logger.Create("Window",       LEVEL.WARN);
    }
}
