using Northwind.DataAccess.Providers;
using Northwind.Models.DbEntities;
using Northwind.ServiceLayer.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox
{
    class Program
    {
        static string con = "Data Source=MINOTEBOOK;Initial Catalog=Task4DB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;Pooling = True;MultipleActiveResultSets=True";
        static async Task Main(string[] args)
        {
            IDataProvider provider = new SqlDataProvider(con);
            DateTime start1 = DateTime.Now;
            IEnumerable<SalesOrderHeader> cust = provider.GetSalesOrderHeaders();
            TimeSpan span1 = DateTime.Now - start1;
            DateTime start2 = DateTime.Now;
            IEnumerable<SalesOrderHeader> cust1 = await provider.GetSalesOrderHeadersAsync();
            TimeSpan span2 = DateTime.Now - start2;
            Console.WriteLine($"Test 1:{DateTime.Now}\nGetSalesOrderHeaders():{span1.TotalMilliseconds}\nGetSalesOrderHeadersAsync():{span2.TotalMilliseconds}\n");
        }
    }
}
