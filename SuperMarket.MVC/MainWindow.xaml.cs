using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SuperMarket.MVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IEnumerable<Product> products { get; set; }
        public int count { get; set; }
        public MainWindow() : this(new ProductCatalog())
        {
            InitializeComponent();
        }

        private MainWindow(IProductCatalog catalog)
        {
            products = catalog.FindAll();
            count = products.Count();
        }

        #if DEBUG
        private static MainWindow MainWindow_FORTESTINGONLY(IProductCatalog catalog)
        {
            return new MainWindow(catalog);
        }
        #endif
    }
}
