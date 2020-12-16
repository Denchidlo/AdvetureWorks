using Northwind.ConfigurationManager.Attributes;
using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Models.DbEntities
{
    [Table(Name = "SalesLT.ProductCategory")]
    public class ProductCategory
    {
        [Column(Name = "ProductCategoryID", IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int ProductCategoryID { get; set; }
        [Column(Name = "ParentProductCategoryID", CanBeNull = true)]
        public int? ParentProductCategoryID { get; set; }
        [Column(Name = "Name")]
        public string Name { get; set; }
        [Column(Name = "rowguid", CanBeNull = false)]
        [ParseIgnore]
        public Guid rowguid { get; set; }
        [Column(Name = "ModifiedDate", CanBeNull = false)]
        [ParseIgnore]
        public DateTime ModifiedDate { get; set; }
    }
}
