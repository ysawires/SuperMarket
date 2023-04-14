namespace SuperMarket.MVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public GroceryViewModel GroceryViewModel { get; }
        public MainWindow() : this(new GroceryViewModel(new ProductCatalog()))
        {
            InitializeComponent();
        }

        private MainWindow(GroceryViewModel groceryViewModel)
        {
            GroceryViewModel = groceryViewModel;
        }
    }
}
