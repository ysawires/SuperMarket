using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.MVC
{
    public sealed class Grocery : NotifyBase
    {
        public Grocery(IProductCatalog catalog)
        {
            Products = catalog.FindAll();
            Count = Products.Count();
            ShoppingCart = new ShoppingCart(Array.Empty<Product>());
        }

        public void AddToCart(Sku sku)
        {
            ShoppingCart += Products.First(product => product.Sku == sku);
            OnPropertyChanged(nameof(ShoppingCart));
        }

        public ShoppingCart ShoppingCart { get; set; }

        public int Count { get; set; }

        public IEnumerable<Product> Products { get; set; }
    }
}