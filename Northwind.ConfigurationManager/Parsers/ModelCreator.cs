using Northwind.ConfigurationManager.Uitls;
using System;
using System.Collections.Generic;

namespace Northwind.ConfigurationManager.Parsers
{
    public class ModelCreator
    {
        public static T CreateInstanse<T>(Dictionary<string, object> pairs)
        {
            T result = (T)Activator.CreateInstance(typeof(T));
            foreach (KeyValuePair<string, object> pair in pairs)
            {
                if (pair.Value.GetType() == typeof(DBNull))
                {
                    ParserUtils.SetMemberValue(result, pair.Key, null);
                }
                else
                {
                    ParserUtils.SetMemberValue(result, pair.Key, pair.Value);
                }

            }
            return result;
        }
    }
}
