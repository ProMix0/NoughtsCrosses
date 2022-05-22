using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossesZeroes.Common
{
    public static class Utils
    {
        public static void LogMessageAndThrowException(this ILogger logger, Exception exception, LogLevel logLevel = LogLevel.Critical)
        {
            logger.Log(logLevel, exception, exception.Message);
            throw exception;
        }
    }
}
