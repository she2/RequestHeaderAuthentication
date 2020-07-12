using System;
using Microsoft.Extensions.Logging;

namespace RequestHeaderAuthentication.Extensions
{
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, Exception> _tokenValidationFailed;
        private static readonly Action<ILogger, Exception> _tokenValidationSucceeded;
        private static readonly Action<ILogger, Exception> _errorProcessingMessage;

        static LoggingExtensions()
        {
            _tokenValidationFailed = LoggerMessage.Define<string>(
                eventId: 1,
                logLevel: LogLevel.Information,
                formatString: "Failed to validate header token {Token}.");
            _tokenValidationSucceeded = LoggerMessage.Define(
                eventId: 2,
                logLevel: LogLevel.Information,
                formatString: "Successfully validated header tokens.");
            _errorProcessingMessage = LoggerMessage.Define(
                eventId: 3,
                logLevel: LogLevel.Error,
                formatString: "Exception occurred while validating header token.");
        }

        public static void TokenValidationFailed(this ILogger logger, string token, Exception ex)
        {
            _tokenValidationFailed(logger, token, ex);
        }

        public static void TokenValidationSucceeded(this ILogger logger)
        {
            _tokenValidationSucceeded(logger, null);
        }

        public static void ErrorProcessingMessage(this ILogger logger, Exception ex)
        {
            _errorProcessingMessage(logger, ex);
        }
    }
}
