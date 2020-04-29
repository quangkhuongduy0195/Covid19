using System;
using System.Diagnostics;
using Covid19.Services.Interfaces;
using Prism.Logging;

namespace Covid19.Services
{
    public class LogService : ILogService
    {
        private static string BuidMessage(string rawMsg, Category category, string caller)
#if DEBUG
            => $"[{DateTime.Now.ToString("HH:mm:ss")}][{category.ToString().ToUpper()}] {caller} {(string.IsNullOrEmpty(rawMsg) ? "..." : rawMsg)}";
#else
            => "meow...";
#endif

        private void LogInternal(string message, Category category, string caller)
        {
#if DEBUG
            var formatedMessage = BuidMessage(message, category, caller);
            Debug.WriteLine(formatedMessage);
#endif
        }
        void ILogService.Log(string message, Category category, string caller)
        {
#if DEBUG
            LogInternal(message, category, caller);
#endif
        }

        void ILoggerFacade.Log(string message, Category category, Priority priority)
        {
#if DEBUG
            var caller = new StackFrame(2).GetMethod().Name;
            LogInternal(message, category, caller);
#endif
        }

        void ILogService.Log(Exception exception)
        {
#if DEBUG
            LogInternal(exception.ToString(), Category.Exception, null);
#endif
        }
    }
}
