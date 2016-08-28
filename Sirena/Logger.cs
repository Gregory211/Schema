using System;
using log4net;
using log4net.Config;

namespace Sirena
{
    public static class Logger
    {
        private static ILog Log { get; set; }

        static Logger()
        {
            XmlConfigurator.Configure();
            Log = LogManager.GetLogger(typeof(Logger));
            Info("Logging is started");
        }

        public static void Error(object msg)
        {
            Log.Error(msg);
        }

        public static void Error(object msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        public static void Error(Exception ex)
        {
            Log.Error(ex.Message, ex);
        }

        public static void Info(object msg)
        {
            Log.Info(msg);
        }
    }
}
