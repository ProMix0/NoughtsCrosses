using Microsoft.Extensions.Logging;

namespace Utils
{
    public static class LogUtils
    {
        /// <summary>
        /// Log <see cref="TException"/> and return it
        /// </summary>
        /// <param name="logger">Logger instance</param>
        /// <param name="exception">Exception to log</param>
        /// <param name="logLevel">Specify <see cref="LogLevel"/>. Default is <see cref="LogLevel.Critical"/></param>
        /// <typeparam name="TException"></typeparam>
        /// <returns>Return exception to further manipulations</returns>
        public static TException LogExceptionMessage<TException>(this TException exception, ILogger logger,
            LogLevel logLevel = LogLevel.Critical)
            where TException : Exception
        {
            logger.Log(logLevel, exception, "Exception was thrown: {Message}", exception.Message);
            return exception;
        }
    }
}