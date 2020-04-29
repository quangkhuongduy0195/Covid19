using System;
using System.Runtime.CompilerServices;
using Prism.Logging;

namespace Covid19.Services.Interfaces
{
    public interface ILogService : ILoggerFacade // to retrieve prism internal message
    {
        void Log(string message = null, Category category = Category.Info, [CallerMemberName]string caller = null);
        void Log(Exception exception);
    }
}
