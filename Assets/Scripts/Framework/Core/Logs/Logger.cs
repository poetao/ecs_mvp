using System.Globalization;
using UnityEngine;

namespace MVP.Framework.Core.Logs
{
    public enum LEVEL
    {
        DEBUG, INFO, WARN, ERROR, FATAL, UNUSE,
    }

    public class LogHandler : ILogHandler
    {
        public void LogException(System.Exception exception, Object context)
        {
            Debug.unityLogger.logHandler.LogException(exception, context);
        }

        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
        }
    }

    public class Logger
    {
        public  static UnityEngine.Logger    logger = new UnityEngine.Logger(new LogHandler());
        private static string[]              levelAbbr = { "D", "I", "W", "E", "F", "U" };
        private        string                category;
        private        LEVEL                 level;
        private        Object                context;
        private        uint                  sn;
        private        bool                  showSn;

        public static Logger Create(string category, LEVEL level, Object context = null)
        {
            return new Logger(category, level, context);
        }

        private static string GetString(object message)
        {
          if (message == null) return "Null";

          var formattable = message as System.IFormattable;
          if (formattable != null) return formattable.ToString(null, CultureInfo.InvariantCulture);

          return message.ToString();
        }

        public Logger(string category, LEVEL level, Object context)
        {
            this.category     = category.ToUpper();
            this.level        = level;
            this.context      = context;
            this.sn           = 0;
            this.showSn       = this.level <= LEVEL.INFO;
        }

        public void D(object message, params object[] args)
        {
            Debug(message, args);
        }

        public void Debug(object message, params object[] args)
        {
            LogFormat(LogType.Log, LEVEL.DEBUG, message, args);
        }

        public void I(object message, params object[] args)
        {
            Info(message, args);
        }

        public void Info(object message, params object[] args)
        {
            LogFormat(LogType.Log, LEVEL.INFO, message, args);
        }

        public void W(object message, params object[] args)
        {
            Warn(message, args);
        }

        public void Warn(object message, params object[] args)
        {
            LogFormat(LogType.Warning, LEVEL.WARN, message, args);
        }

        public void E(object message, params object[] args)
        {
            Error(message, args);
        }

        public void Error(object message, params object[] args)
        {
            LogFormat(LogType.Error, LEVEL.ERROR, message, args);
        }

        public void F(object message, params object[] args)
        {
            Fatal(message, args);
        }

        public void Fatal(object message, params object[] args)
        {
            LogFormat(LogType.Error, LEVEL.FATAL, message, args);
        }

        public void Exception(System.Exception e)
        {
            logger.LogException(e, context);
        }

        private void LogFormat(LogType type, LEVEL level, object message, params object[] args)
        {
            if (level < this.level || this.level == LEVEL.UNUSE) return;

            var fmt = showSn ? "[{0:0000}-{1}-{2}]#" : "[{1}-{2}]";
            var preStr = string.Format(fmt, sn, levelAbbr[(int)level], category);
            sn += 1;
            if (args.Length <= 0)
            {
                logger.LogFormat(type, context, "{0}", $"{preStr} {GetString(message)}");
                return;
            }

            logger.LogFormat(type, context, $"{preStr}: {GetString(message)}", args);
        }
    }
}