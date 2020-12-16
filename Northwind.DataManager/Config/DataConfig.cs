using Northwind.ConfigurationManager.Parsers;
using Northwind.DataAccess.Providers;
using System.IO;

namespace Northwind.DataManager.Config
{
    public class DataConfig
    {
        public ProviderType Type { get; set; }
        public string ConnectionString { get; set; }
        public string TargetPath { get; set; }
        public static DataConfig GetConfiguration(string src)
        {
            using (var file = File.Open(src, FileMode.Open))
            {
                StreamReader reader = new StreamReader(file);
                string json = reader.ReadToEnd();
                reader.Close();
                IDeserializer deserializer = new JsonParser();
                DataConfig configuration = deserializer.Deserialize<DataConfig>(json);
                return configuration;
            }
        }
        public void SetConfiguration(string src)
        {
            using (var file = File.Open(src, FileMode.Create))
            {
                StreamWriter writer = new StreamWriter(file);
                ISerrializer serrializer = new JsonParser();
                string xml = serrializer.Serialize(this);
                writer.Write(xml);
                writer.Close();
            }
        }
    }
}
