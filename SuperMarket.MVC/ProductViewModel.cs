#nullable enable
using System.Windows.Input;

namespace SuperMarket.MVC
{
    public sealed class ProductViewModel
    {
        private readonly Product _product;

        public ProductViewModel(ICommand addProductToCart, Product product)
        {
            AddProductToCart = addProductToCart;
            _product = product;
        }

        public ICommand AddProductToCart { get; }
        public Sku Sku => _product.Sku;
        public UnitPrice UnitPrice => _product.UnitPrice;
        public SpecialPrice? SpecialPrice => _product.SpecialPrice;

        public static explicit operator Product(ProductViewModel viewModel) => viewModel._product;
    }
}