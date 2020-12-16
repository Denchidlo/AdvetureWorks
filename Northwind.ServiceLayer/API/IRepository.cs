﻿using Northwind.Models;
using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.ServiceLayer.API
{
    public interface IRepository
    {
        IEnumerable<Customer> Customers { get; }
        IEnumerable<CustomerAddress> CustomerAddresses{ get; }
        IEnumerable<Address> Addresses { get; }
        IEnumerable<Product> Products{ get; }
        IEnumerable<ProductCategory> ProductCategories { get; }
        IEnumerable<SalesOrderDetail> SalesOrderDetails { get; }
        IEnumerable<SalesOrderHeader> SalesOrderHeaders { get; }
        IEnumerable<DetailedSummary> DetailedSummaries{ get; }
        void Update();
        void CalculateSummary();
        DetailedSummary GetDetailedSummary(int orderId);
        Customer GetCustomer(int Id);
        Address GetCustomerAddress(int customerId);
        Address GetAddress(int addressId);
        Product GetProduct(int id);
        IEnumerable<Product> GetProducts(int categoryId);
        ProductCategory GetProductCategory(int id);
        SalesOrderHeader GetSalesOrderHeader(int id);
    }
}