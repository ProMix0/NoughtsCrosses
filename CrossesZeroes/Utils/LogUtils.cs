using Microsoft.Extensions.Logging;

namespace CrossesZeroes.Utils
{
    public static class LogUtils
    {
        public static TException LogExceptionMessage<TException>(this ILogger logger, TException exception, LogLevel logLevel = LogLevel.Critical)
            where TException : Exception
        {
            logger.Log(logLevel, exception, "Exception was thrown: {Message}", exception.Message);
            return exception;
        }
    }
}
