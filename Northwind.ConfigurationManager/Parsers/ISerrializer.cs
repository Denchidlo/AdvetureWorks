using System.Threading.Tasks;

namespace Northwind.ConfigurationManager.Parsers
{
    public interface ISerializer
    {
        string Serialize(object obj);
        Task<string> SerializeAsync(object obj);
    }
}
