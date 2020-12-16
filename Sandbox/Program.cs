using Northwind.ConfigurationManager.Parsers;
using Northwind.DataAccess.Providers;
using Northwind.DataManager;
using Northwind.DataManager.Config;
using Northwind.FileManager;
using Northwind.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading;

namespace Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            

            DirectoryInfo info = new DirectoryInfo(Environment.CurrentDirectory);
            string path = info.Parent.Parent.FullName + "\\appsettings.json";
            DataConfig config = new DataConfig();
            config.ConnectionString = @"Data Source=MINOTEBOOK;Initial Catalog=Task4DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False; Pooling = True";
            config.TargetPath = "C\\Target";
            config.Type = ProviderType.LinqProvider;
            DataManager manager = new DataManager(config);
            ISerrializer serrializer = new XmlParser();
            Console.WriteLine(serrializer.Serialize(manager.Repository.DetailedSummaries.First()));
        }
    }
}
