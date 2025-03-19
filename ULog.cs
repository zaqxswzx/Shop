
using NLog;

namespace Shop {
    public static class ULog {
        public static readonly Logger dbLogger = LogManager.GetLogger("logdb");
        public static SetLogType Web => new(LogType.Web);
        public static SetLogType DB => new(LogType.DB);

        public class SetLogType {
            private LogType _logType;
            public SetLogType(LogType logType) {
                _logType = logType;
            }
            public void Info(string reqMessage, string message, Exception? exception = null) {
                this.setLog(reqMessage, message, NLog.LogLevel.Info, exception);
            }
            public void Warn(string reqMessage, string message, Exception? exception = null) {
                this.setLog(reqMessage, message, NLog.LogLevel.Warn, exception);
            }
            public void Error(string reqMessage, string message, Exception? exception = null) {
                this.setLog(reqMessage, message, NLog.LogLevel.Error, exception);
            }
            private void setLog(string reqMessage, string message, NLog.LogLevel logLevel, Exception? exception = null) {
                LogEventInfo logEventInfo = new LogEventInfo(logLevel, dbLogger.Name, message);
                logEventInfo.Properties["logType"] = _logType.ToString();
                if (!string.IsNullOrEmpty(reqMessage)) {
                    logEventInfo.Properties["RequestMessage"] = reqMessage.ToString();
                }
                logEventInfo.Exception = exception;
                dbLogger.Log(logEventInfo);
            }
        }
    }
}
