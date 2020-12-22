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
        Task<IEnumerable<ProductCategory>> GetProductCategoriesAsync();
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<CustomerAddress>> GetCustomerAddressesAsync();
        Task<IEnumerable<Address>> GetAddressesAsync();
        Task<IEnumerable<SalesOrderDetail>> GetSalesOrderDetailsAsync();
        Task<IEnumerable<SalesOrderHeader>> GetSalesOrderHeadersAsync();
    }
}
