using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.ConfigurationManager.Parsers
{
    public interface IDeserializer
    {
        T Deserialize<T>(string stringRepresentatoin);
    }
}
