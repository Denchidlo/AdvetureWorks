using Northwind.ConfigurationManager.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Models.DbEntities
{
    [Table(Name  = "SalesLT.SalesOrderDetail")]
    public class SalesOrderDetail
    {
        [Column(Name = "SalesOrderID", IsPrimaryKey = true, CanBeNull = false)]
        public int SalesOrderID { get; set; }
        [Column(Name = "SalesOrderDetailID", IsPrimaryKey = true, CanBeNull = false)]
        public int SalesOrderDetailID { get; set; }
        [Column(Name = "OrderQty")]
        public Int16 OrderQty { get; set; }
        [Column(Name = "ProductID")]
        public int ProductID { get; set; }/*
        [Column(Name = "UnitPrice")]
        public double UnitPrice { get; set; }
        [Column(Name = "UnitPriceDiscount")]
        public double UnitPriceDiscount { get; set; }*/
        [Column(Name = "LineTotal", IsDbGenerated = true)]
        public decimal LineTotal { get; set; }
        [Column(Name = "rowguid", CanBeNull = false)]
        [ParseIgnore]
        public Guid rowguid { get; set; }
        [Column(Name = "ModifiedDate", CanBeNull = false)]
        [ParseIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
