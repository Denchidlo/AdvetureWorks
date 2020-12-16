using Northwind.Models.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Northwind.DataExplorer.Windows
{
    /// <summary>
    /// Interaction logic for ProductObserver.xaml
    /// </summary>
    public partial class ProductObserver : Window
    {
        public ProductObserver(Product product)
        {
            InitializeComponent();
            TB1.Text = $"Name: {product.Name}\n" +
                        $"Product number {product.ProductNumber}\n" +
                        $"Size: {product.Size}\n" +
                        $"Published {product.SellStartDate}";
        }
    }
}
