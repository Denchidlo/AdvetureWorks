using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Providers
{
    public class LinqDataProvider : IDataProvider, IDisposable
    {
        DataContext db;
        public LinqDataProvider(string connectionString)
        {
            this.db = new DataContext(connectionString);
        }
        public IEnumerable<Address> GetAddresses()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<Address> res = db.GetTable<Address>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetAddresses() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<Customer> GetCustomers()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<Customer> res = db.GetTable<Customer>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetCustomers() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<CustomerAddress> GetCustomerAddresses()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<CustomerAddress> res = db.GetTable<CustomerAddress>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetCustomerAddresses() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<Product> res = db.GetTable<Product>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetProducts() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<ProductCategory> res = db.GetTable<ProductCategory>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
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
                    List<SalesOrderHeader> res = db.GetTable<SalesOrderHeader>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetSalesOrderHeaders() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public IEnumerable<SalesOrderDetail> GetSalesOrderDetails()
        {
            if (db.DatabaseExists())
            {
                try
                {
                    List<SalesOrderDetail> res = db.GetTable<SalesOrderDetail>().ToList();
                    return res;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Loading failed in GetSalesOrderDetails() \n{ex.Message}");
                }
            }
            else
            {
                throw new Exception("DB connection failed");
            }
        }
        public void Dispose()
        {
            this.db.Dispose();
        }
        ~LinqDataProvider()
        {
            this.Dispose();
        }
    }
}
