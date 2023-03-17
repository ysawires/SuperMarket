using System.Windows;
using System.Windows.Controls;

namespace SuperMarket.MVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public Grocery Grocery { get; }
        public MainWindow() : this(new Grocery(new ProductCatalog()))
        {
            InitializeComponent();
        }

        private MainWindow(Grocery grocery)
        {
            Grocery = grocery;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is Button button && button.Tag is Sku sku){
                Grocery.AddToCart(sku);
            }
        }


#if DEBUG
        private static MainWindow MainWindow_FORTESTINGONLY(IProductCatalog catalog)
        {
            return new MainWindow(new Grocery(catalog));
        }
#endif
    }
}
