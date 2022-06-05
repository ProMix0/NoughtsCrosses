using Microsoft.Extensions.Logging;

namespace CrossesZeroes.Utils
{
    public static class LogUtils
    {
        public static void LogMessageAndThrow(this ILogger logger, Exception exception, LogLevel logLevel = LogLevel.Critical)
        {
            logger.Log(logLevel, exception,"Exception was thrown: {Message}", exception.Message);
            throw exception;
        }
    }
}
