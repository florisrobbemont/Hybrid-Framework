using System;
using System.Web;
using Umbraco.Core.Logging;
using UmbracoTemplate.Logging;

namespace UmbracoTemplate.Web.Logging
{
    public class LoggingService : ILoggingService
    {
        public HttpContextBase HttpContextBase { get; set; }

        public void Log(string message)
        {
            LogHelper.Info<LoggingService>(message);
        }

        public void Log(string message, LogTypes type)
        {
            switch (type)
            {
                case LogTypes.Debug:
                    Debug(message);
                    return;
                case LogTypes.Error:
                    Error(message);
                    return;
                case LogTypes.Fatal:
                    Fatal(message);
                    return;
                case LogTypes.Info:
                    Info(message);
                    return;
                case LogTypes.Trace:
                    if (HttpContextBase != null) HttpContextBase.Trace.Write(message);
                    return;
                case LogTypes.Warn:
                    Warn(message);
                    return;
                default:
                    return;
            }
        }

        public void Info(string message, params object[] args)
        {
            LogHelper.Info<LoggingService>(string.Format(message, args));
        }

        public void Debug(string message, params object[] args)
        {
            LogHelper.Debug<LoggingService>(string.Format(message, args));
        }

        public void Warn(string message, params object[] args)
        {
            LogHelper.Warn<LoggingService>(string.Format(message, args));
        }

        public void Error(string message, params object[] args)
        {
            LogHelper.Error<LoggingService>(string.Format(message, args), new Exception(string.Format(message, args)));
            LogElmah(new Exception(string.Format(message, args)), string.Format(message, args));
        }

        public void Error(string message, Exception exception, params object[] args)
        {
            LogHelper.Error<LoggingService>(string.Format(message, args), exception);
            LogElmah(exception, string.Format(message, args));
        }

        public void Fatal(string message, params object[] args)
        {
            LogHelper.Error<LoggingService>(string.Format(message, args), new Exception(string.Format(message, args)));
            LogElmah(new Exception(string.Format(message, args)), string.Format(message, args));
        }

        public void Fatal(string message, Exception exception, params object[] args)
        {
            LogHelper.Error<LoggingService>(string.Format(message, args), exception);
            LogElmah(exception, string.Format(message, args));
        }

        private void LogElmah(Exception ex, string customMessage)
        {
            var error = new Elmah.Error(ex);

            if (!string.IsNullOrEmpty(customMessage))
            {
                error.Detail = customMessage;
            }

            if (HttpContextBase != null)
                Elmah.ErrorLog.GetDefault(HttpContextBase.ApplicationInstance.Context).Log(error);
        }
    }
}