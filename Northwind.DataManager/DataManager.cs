using Northwind.ConfigurationManager.Parsers;
using Northwind.DataAccess.Providers;
using Northwind.DataManager.Config;
using Northwind.ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataManager
{
    public class DataManager
    {
        DataConfig config;

        public IRepository Repository { get; private set; }

        public DataManager()
        {
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            string path = info.Parent.Parent.FullName + "\\appsettings.json";
            config = DataConfig.GetConfiguration(path);
            Repository = new Database(config.ConnectionString, ProviderType.LinqProvider);
            Repository.CalculateSummary();
        }

        public DataManager(DataConfig cfg)
        {
            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            string path = info.Parent.Parent.FullName + "\\appsettings.json";
            this.config = cfg;
            cfg.SetConfiguration(path);
            Repository = new Database(config.ConnectionString, ProviderType.LinqProvider);
            Repository.CalculateSummary();
        }

        public void MakeLog()
        {
            ISerrializer serrializer = new XmlParser();
            string s = serrializer.Serialize(Repository.DetailedSummaries);
            using (StreamWriter sw = new StreamWriter($"{config.TargetPath}\\file_{DateTime.Now}.xml"))
            {
                sw.Write(s);
            }
        }
    }
}
