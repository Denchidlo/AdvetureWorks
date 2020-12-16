using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Northwind.DataAccess.Providers
{
    public class LinqDataProvider : IDataProvider, IDisposable, ILogger
    {
        DataContext db;
        public LinqDataProvider(string connectionString)
        {
            this.db = new DataContext(connectionString);
            Log(DateTime.Now, "Connected to server by LinqDataProvider");
        }
        public IEnumerable<Address> GetAddresses()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetAddresses() was invoked");
                    List<Address> res = db.GetTable<Address>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetAddresses() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetAddresses() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<Customer> GetCustomers()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetCustomers() was invoked");
                    List<Customer> res = db.GetTable<Customer>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetCustomers() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetCustomers() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<CustomerAddress> GetCustomerAddresses()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetCustomerAddresses() was invoked");
                    List<CustomerAddress> res = db.GetTable<CustomerAddress>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetCustomerAddresses() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetCustomerAddresses() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetProducts() was invoked");
                    List<Product> res = db.GetTable<Product>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetProducts() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetProducts() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetProductCategories() was invoked");
                    List<ProductCategory> res = db.GetTable<ProductCategory>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetSalesOrderHeaders() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetProductCategories() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<SalesOrderHeader> GetSalesOrderHeaders()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetSalesOrderHeaders() was invoked");
                    List<SalesOrderHeader> res = db.GetTable<SalesOrderHeader>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetSalesOrderHeaders() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetSalesOrderHeaders() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<SalesOrderDetail> GetSalesOrderDetails()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    Log(DateTime.Now, "GetSalesOrderDetails() was invoked");
                    List<SalesOrderDetail> res = db.GetTable<SalesOrderDetail>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    Log(DateTime.Now, $"Loading failed in GetSalesOrderDetails() \n{ex.Message}");
                    throw new Exception($"Loading failed in GetSalesOrderDetails() \n{ex.Message}");
                }
            }
            else
            {
                Log(DateTime.Now, "DB connection failed");
                throw new Exception("DB connection failed");
            }
        }
        public void Log(DateTime date, string message)
        {
            SqlCommand command = new SqlCommand("SendLog", db.Connection as SqlConnection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.Add(new SqlParameter("date", date));
            command.Parameters.Add(new SqlParameter("message", message));

            using (TransactionScope scope = new TransactionScope())
            {
                command.ExecuteNonQuery();
                scope.Complete();
            }
        }
        public void Dispose()
        {
            Log(DateTime.Now, "Disconnected from server as LinqDataProvider");
            this.db.Dispose();
        }
        ~LinqDataProvider()
        {
            this.Dispose();
        }
    }
}
