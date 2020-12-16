using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Providers
{
    public interface ILogger
    {
        void Log(DateTime time, string message);
    }
}
