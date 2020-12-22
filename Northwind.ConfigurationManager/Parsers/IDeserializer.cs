using System.Threading.Tasks;

namespace Northwind.ConfigurationManager.Parsers
{
    public interface IDeserializer
    {
        T Deserialize<T>(string stringRepresentatoin);
        Task<T> DeserializeAsync<T>(string stringRepresentatoin);
    }
}
