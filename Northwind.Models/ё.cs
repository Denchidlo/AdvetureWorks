using System;
using System.Data.Linq.Mapping;
using Northwind.ConfigurationManager.Attributes;

namespace Northwind.Models
{
    [Table(Name = "SalesLT.CustomerAddress")]
    public class CustomerAddress
    {
        [Column(Name = "CustomerID", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int CustomerID { get; set; }
        [Column(Name = "AddressID", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int AddressID { get; set; }
        [Column(Name = "AddressType", CanBeNull = false)]
        public string AddressType { get; set; }
        [Column(Name = "rowguid")]
        [ParseIgnore]
        public Guid rowguid { get; set; }
        [Column(Name = "ModifiedDate")]
        [ParseIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
