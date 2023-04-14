using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace SuperMarket.MVC
{
    
    public sealed partial class GroceryViewModel : NotifyBase
    {
        public GroceryViewModel(IProductCatalog catalog)
        {
            ShoppingCart = new ShoppingCart(Array.Empty<Product>());
            Products = catalog.FindAll().Select(product => new ProductViewModel(AddToCartCommand, product));
            Count = Products.Count();
        }

        [RelayCommand]
        private void AddToCart(Sku? sku)
        {
            ShoppingCart += (Product) Products.First(product => product.Sku == sku);
            OnPropertyChanged(nameof(ShoppingCart));
        }

        public ShoppingCart ShoppingCart { get; private set; }

        public int Count { get; }

        public IEnumerable<ProductViewModel> Products { get; }
    }
}