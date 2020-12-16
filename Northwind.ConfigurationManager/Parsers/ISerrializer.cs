using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.ConfigurationManager.Parsers
{
    public interface ISerrializer
    {
        string Serialize(object obj);
    }
}
