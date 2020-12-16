using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.DataAccess.Providers
{
    public enum ProviderType
    {
        LinqProvider,
        SqlProvider
    };

    public interface IDataProvider
    {
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<Product> GetProducts();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<CustomerAddress> GetCustomerAddresses();
        IEnumerable<Address> GetAddresses();
        IEnumerable<SalesOrderDetail> GetSalesOrderDetails();
        IEnumerable<SalesOrderHeader> GetSalesOrderHeaders();
    }
}
