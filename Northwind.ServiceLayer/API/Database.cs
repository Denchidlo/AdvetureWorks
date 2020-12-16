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
            //provider = type == ProviderType.LinqProvider ? new LinqDataProvider(connectionString) : new SqlDataProvider(connectionString);
            provider = new LinqDataProvider(connectionString);
            this.Update();            
        }
        public void Update()
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
                //temp.LineTotal = order.LineTotal;
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
                if (el.CustomerId == id)
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
