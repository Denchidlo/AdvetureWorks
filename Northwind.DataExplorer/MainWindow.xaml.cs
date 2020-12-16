using Northwind.DataExplorer.Windows;
using Northwind.DataManager;
using Northwind.FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Northwind.DataExplorer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Logger logger;
        DataManager.DataManager manager;
        void PreInit()
        {
            ListViewItem first = new ListViewItem();
            first.Tag = (int)-1;
            first.Content = "...";
            ProductCategories.Items.Add(first);
            foreach (var el in manager.Repository.ProductCategories)
            {
                ListViewItem tmp = new ListViewItem();
                tmp.Tag = el.ProductCategoryID;
                tmp.Content = el.Name;
                ProductCategories.Items.Add(tmp);
            }
            foreach (var el in manager.Repository.Products)
            {
                ListViewItem tmp = new ListViewItem();
                tmp.Tag = el.ProductID;
                tmp.Content = el.Name;
                Products.Items.Add(tmp);
            }
        }
        public MainWindow()
        {
            logger = new Logger();
            Thread thread = new Thread(new ThreadStart(logger.Start));
            thread.Start();
            InitializeComponent();
            try
            {
                manager = new DataManager.DataManager();
                manager.MakeLog();
                
                PreInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MainWindow() :\n" + ex.Message);
            }
        }

        private void Products_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                int id = (int)(((ListViewItem)Products.SelectedItem).Tag);
                ProductObserver observer = new ProductObserver(manager.Repository.GetProduct(id));
                observer.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item_MouseDoubleClick(object sender, MouseButtonEventArgs e) :\n" + ex.Message);
            }
        }

        private void ProductCategories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBoxItem item = (ListBoxItem)ProductCategories.SelectedItem;
                int id = (int)(item.Tag);
                if (id == -1)
                {
                    Products.Items.Clear();
                    foreach (var el in manager.Repository.Products)
                    {
                        ListViewItem tmp = new ListViewItem();
                        tmp.Tag = el.ProductID;
                        tmp.Content = el.Name;
                        Products.Items.Add(tmp);
                    }
                }
                else
                {
                    Products.Items.Clear();
                    foreach (var el in manager.Repository.Products.Where(ell => ell.ProductCategoryID == id))
                    {
                        ListViewItem tmp = new ListViewItem();
                        tmp.Tag = el.ProductID;
                        tmp.Content = el.Name;
                        Products.Items.Add(tmp);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Item_MouseDoubleClick(object sender, MouseButtonEventArgs e) :\n" + ex.Message);
            }
        }
    }
}
