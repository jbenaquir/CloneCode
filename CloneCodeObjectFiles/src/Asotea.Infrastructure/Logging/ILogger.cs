using System;

namespace Asotea.Infrastructure.Logging
{
    public interface ILogger
    {
        void Trace(string message, params object[] args);
        void Trace(string message);
        void Debug(string message, params object[] args);
        void Debug(string message);
        void Info(string message, params object[] args);
        void Info(string message);
        void Warn(string message, params object[] args);
        void Warn(string message);
        void Error(Exception exception, string message, params object[] args);
        void Error(Exception exception, string message);
        void Error(Exception exception);
        void Error(string message, params object[] args);
        void Error(string message);
        void Fatal(Exception exception, string message, params object[] args);
        void Fatal(Exception exception, string message);
        void Fatal(Exception exception);
        void Fatal(string message, params object[] args);
        void Fatal(string message);
    }
}