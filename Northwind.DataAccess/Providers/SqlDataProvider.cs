using Northwind.ConfigurationManager.Parsers;
using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Northwind.DataAccess.Providers
{
    public class SqlDataProvider : IDataProvider, IDisposable, ILogger
    {
        SqlConnection connection;
        public SqlDataProvider(string connectionString)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                connection = new SqlConnection(connectionString);
                connection.Open();
                Log(DateTime.Now, "Connected to server by LinqDataProvider");
                scope.Complete();
            }
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            try
            {
                Log(DateTime.Now, "GetProductCategories() was invoked");
                SqlCommand command = new SqlCommand("GetProductCategories", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<ProductCategory> ans = Map<ProductCategory>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetProductCategories() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<Product> GetProducts()
        {
            try
            {
                Log(DateTime.Now, "GetProducts() was invoked");
                SqlCommand command = new SqlCommand("GetProducts", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<Product> ans = Map<Product>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetProducts() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<Customer> GetCustomers()
        {
            try
            {
                Log(DateTime.Now, "GetCustomers() was invoked");
                SqlCommand command = new SqlCommand("GetCustomers", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<Customer> ans = Map<Customer>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetCustomers() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<CustomerAddress> GetCustomerAddresses()
        {
            try
            {
                Log(DateTime.Now, "GetCustomerAddresses() was invoked");
                SqlCommand command = new SqlCommand("GetCustomerAddresses", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<CustomerAddress> ans = Map<CustomerAddress>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetCustomerAddresses() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<Address> GetAddresses()
        {
            try
            {
                Log(DateTime.Now, "GetAddresses() was invoked");
                SqlCommand command = new SqlCommand("GetAddresses", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<Address> ans = Map<Address>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetAddresses() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<SalesOrderDetail> GetSalesOrderDetails()
        {
            try
            {
                Log(DateTime.Now, "GetSalesOrderDetails() was invoked");
                SqlCommand command = new SqlCommand("GetSalesOrderDetails", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<SalesOrderDetail> ans = Map<SalesOrderDetail>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetSalesOrderDetails() \n{ex.Message}");
                throw ex;
            }
        }
        public IEnumerable<SalesOrderHeader> GetSalesOrderHeaders()
        {
            try
            {
                Log(DateTime.Now, "GetSalesOrderHeaders() was invoked");
                SqlCommand command = new SqlCommand("GetSalesOrderHeaders", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                using (TransactionScope scope = new TransactionScope())
                {
                    List<SalesOrderHeader> ans = Map<SalesOrderHeader>(command.ExecuteReader());
                    scope.Complete();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                Log(DateTime.Now, $"Loading failed in GetSalesOrderHeaders() \n{ex.Message}");
                throw ex;
            }
        }
        List<Dictionary<string, object>> Parse(SqlDataReader reader)
        {
            List<Dictionary<string, object>> ans = new List<Dictionary<string, object>>();
            while (reader.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string name = reader.GetName(i);
                    object val = reader.GetValue(i);
                    dict.Add(name, val);
                }
                ans.Add(dict);
            }
            reader.Close();
            return ans;
        }
        List<T> Map<T>(SqlDataReader reader)
        {
            List<Dictionary<string, object>> parsed = Parse(reader);
            List<T> ans = new List<T>();
            foreach (Dictionary<string, object> dict in parsed)
            {
                ans.Add(ModelCreator.CreateInstanse<T>(dict));
            }
            return ans;
        }
        public void Log(DateTime date, string message)
        {
            SqlCommand command = new SqlCommand("SendLog", connection);
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
            //connection.Close();
        }
        ~SqlDataProvider()
        {
            Dispose();
        }
    }
}
