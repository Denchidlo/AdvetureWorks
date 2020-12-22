using Northwind.DataAccess.Providers;
using Northwind.Models;
using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.ServiceLayer.API
{
    public class Database : IRepository, IDisposable
    {
        IDataProvider provider;
        public Database(string connectionString, ProviderType type)
        {
            provider = type == ProviderType.LinqProvider ? (IDataProvider)(new LinqDataProvider(connectionString)) : (IDataProvider)(new SqlDataProvider(connectionString));
            this.Update();            
        }
        public void Update()
        {
            try
            {
                Task<IEnumerable<Customer>> customers = provider.GetCustomersAsync();
                Task<IEnumerable<CustomerAddress>> customerAddresses = provider.GetCustomerAddressesAsync();
                Task<IEnumerable<Address>> addresses = provider.GetAddressesAsync();
                Task<IEnumerable<Product>> products = provider.GetProductsAsync();
                Task<IEnumerable<ProductCategory>> productCategories = provider.GetProductCategoriesAsync();
                Task<IEnumerable<SalesOrderDetail>> salesDetails = provider.GetSalesOrderDetailsAsync();
                Task<IEnumerable<SalesOrderHeader>> salesHeaders = provider.GetSalesOrderHeadersAsync();
                List<Task> tasks = new List<Task> { customers, customerAddresses,
                                                    addresses, products,
                                                    productCategories, salesDetails,
                                                    salesHeaders};
                while(tasks.Count > 0)
                {
                    Task complete = (Task)tasks.Where(e => e.IsCompleted == true).FirstOrDefault();
                    if (complete == customers)
                        this.Customers = customers.Result;
                    else if (complete == customerAddresses)
                        this.CustomerAddresses = customerAddresses.Result;
                    else if (complete == addresses)
                        this.Addresses = addresses.Result;
                    else if (complete == products)
                        this.Products = products.Result;
                    else if (complete == productCategories)
                        this.ProductCategories = productCategories.Result;
                    else if (complete == salesDetails)
                        this.SalesOrderDetails = salesDetails.Result;
                    else if (complete == salesHeaders)
                        this.SalesOrderHeaders = salesHeaders.Result;
                    tasks.Remove(complete);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occured in Database.UpdateAsync()\n{ex.Message}");
            }  
        }
        public void UpdateDeprecated()
        {
            try
            {
                this.Customers = provider.GetCustomers();
                this.CustomerAddresses = provider.GetCustomerAddresses();
                this.Addresses = provider.GetAddresses();
                this.Products = provider.GetProducts();
                this.ProductCategories = provider.GetProductCategories();
                this.SalesOrderDetails = provider.GetSalesOrderDetails();
                this.SalesOrderHeaders = provider.GetSalesOrderHeaders();
            }
            catch(Exception ex)
            {
                throw new Exception($"Error occured in Database.Update()\n{ex.Message}");
            }
        }
        public async void UpdateAsync()
        {
            await Task.Run(() => Update());
        }
        public async void UpdateDeprecatedAsync()
        {
            await Task.Run(() => UpdateDeprecated());
        }
        public IEnumerable<Customer> Customers { get; private set; }
        public IEnumerable<CustomerAddress> CustomerAddresses { get; private set; }
        public IEnumerable<Address> Addresses { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public IEnumerable<ProductCategory> ProductCategories { get; private set; }
        public IEnumerable<SalesOrderDetail> SalesOrderDetails { get; private set; }
        public IEnumerable<SalesOrderHeader> SalesOrderHeaders { get; private set; }
        public IEnumerable<DetailedSummary> DetailedSummaries { get; private set; }
        public IEnumerable<Product> GetProducts(int categoryId)
        {
            IEnumerable<Product> responce = Products.Where(e => e.ProductCategoryID == categoryId);
            return responce.Count() == 0 ? null : responce;
        }
        public void CalculateSummary()
        {
            List<DetailedSummary> list = new List<DetailedSummary>();
            foreach (var order in SalesOrderDetails)
            {
                DetailedSummary temp = new DetailedSummary();
                temp.SalesOrderID = order.SalesOrderID;
                temp.ProductId = order.ProductID;
                Product product = GetProduct(temp.ProductId);
                temp.ProductName = product.Name;
                temp.ProductNumber = product.ProductNumber;
                temp.ProductSize = product.Size;
                temp.ProductCategory = product.ProductCategoryID.HasValue ? GetProductCategory(product.ProductCategoryID.Value).Name : null;
                SalesOrderHeader header = GetSalesOrderHeader(order.SalesOrderID);
                temp.Status = header.Status;
                temp.ShipMethod = header.ShipMethod;
                temp.OwnerId = header.CustomerID;
                Address shipAddress = GetAddress(header.ShipToAddressID);
                temp.ShipAddressCity = shipAddress.City;
                temp.ShipAddressLine = shipAddress.AddressLine1;
                Customer owner = GetCustomer(header.CustomerID);
                temp.OwnerTitle = owner.Title;
                temp.OwnerFirstName = owner.FirstName;
                temp.OwnerLastName = owner.LastName;
                temp.OwnerMiddleName = owner.MiddleName;
                temp.OwnerSuffix = owner.Suffix;
                temp.OwnerSalesPerson = owner.SalesPerson;
                temp.OwnerCompanyName = owner.CompanyName;
                temp.OwnerPhone = owner.Phone;
                temp.OwnerCompanyName = owner.CompanyName;
                temp.OwnerEmailAddress = owner.EmailAddress;
                list.Add(temp);
            }
            this.DetailedSummaries = list;
        }
        public async void CalculateSummaryAsync()
        {
            await Task.Run(() => CalculateSummary());
        }
        public DetailedSummary GetDetailedSummary(int id)
        {
            foreach (var summary in DetailedSummaries)
                if (summary.SalesOrderID == id)
                    return summary;
            return null;
        }
        public Customer GetCustomer(int id)
        {
            foreach (var el in Customers)
                if (el.CustomerID == id)
                    return el;
            return null;
        }
        public Address GetCustomerAddress(int customerId)
        {
            foreach (var el in CustomerAddresses)
                if (el.CustomerID == customerId)
                    return GetAddress(el.AddressID);           
            return null;
        }
        public Address GetAddress(int id)
        {
            foreach (var el in Addresses)
                if (el.AddressID == id)
                    return el;
            return null;
        }
        public Product GetProduct(int id)
        {
            foreach (var el in Products)
                if (el.ProductID == id)
                    return el;
            return null;
        }
        public ProductCategory GetProductCategory(int id)
        {
            foreach (var el in ProductCategories)
                if (el.ProductCategoryID == id)
                    return el;
            return null;
        }
        public SalesOrderHeader GetSalesOrderHeader(int id)
        {
            foreach (var el in SalesOrderHeaders)
                if (el.SalesOrderID == id)
                    return el;
            return null;
        }
        public SalesOrderDetail GetSalesOrderDetail(int id)
        {
            foreach (var el in SalesOrderDetails)
                if (el.SalesOrderID == id)
                    return el;
            return null;
        }
        public async Task<DetailedSummary> GetDetailedSummaryAsync(int orderId)
        {
            return await Task.Run(() => GetDetailedSummary(orderId));
        }
        public async Task<Customer> GetCustomerAsync(int Id)
        {
            return await Task.Run(() => GetCustomer(Id));
        }
        public async Task<Address> GetCustomerAddressAsync(int customerId)
        {
            return await Task.Run(() => GetCustomerAddress(customerId));
        }
        public async Task<Address> GetAddressAsync(int addressId)
        {
            return await Task.Run(() => GetAddress(addressId));
        }
        public async Task<Product> GetProductAsync(int id)
        {
            return await Task.Run(() => GetProduct(id));
        }
        public async Task<IEnumerable<Product>> GetProductsAsync(int categoryId)
        {
            return await Task.Run(() => GetProducts(categoryId));
        }
        public async Task<ProductCategory> GetProductCategoryAsync(int id)
        {
            return await Task.Run(() => GetProductCategory(id));
        }
        public async Task<SalesOrderHeader> GetSalesOrderHeaderAsync(int id)
        {
            return await Task.Run(() => GetSalesOrderHeader(id));
        }
        public async Task<SalesOrderDetail> GetSalesOrderDetailAsync(int id)
        {
            return await Task.Run(() => GetSalesOrderDetail(id));
        }
        public void Dispose()
        {
            if (provider is IDisposable)
                ((IDisposable)provider).Dispose();
        }
        ~Database()
        {
            this.Dispose();
        }
    }
}
