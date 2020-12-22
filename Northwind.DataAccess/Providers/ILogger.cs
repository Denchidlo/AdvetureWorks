using System;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Providers
{
    public interface ILogger
    {
        void Log(DateTime time, string message);
        void LogAsync(DateTime time, string message);
    }
}
