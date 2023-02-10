using System.Collections.Generic;
using System.Linq;

namespace SuperMarket
{

    public interface IProductCatalog
    {
        IMaybe FindOneBySku(Sku sku);
        IEnumerable<Product> FindAll();
    };

    public class ProductCatalog : IProductCatalog
    {
        // Item  Price   Offer
        // --------------------------
        // A     50       3 for 130
        // B     30       2 for 45
        // C     20
        // D     15
        public List<Product> products = new List<Product>()
        {
            new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130)),
            new Product(new Sku("B"), new UnitPrice(30), new SpecialPrice(2, 45)),
            new Product(new Sku("C"), new UnitPrice(20), null),
            new Product(new Sku("D"), new UnitPrice(15), null),
        };
        public IMaybe FindOneBySku(Sku sku)
        {
            return IMaybe.Of(products.Find(product => product.Sku == sku));
        }

        public IEnumerable<Product> FindAll()
        {
            return products;
        }
    }

    public record Sku(string Identifier);

    public record UnitPrice(uint Value)
    {
        public static explicit operator int(UnitPrice unitPrice) => (int)unitPrice.Value;
    };

    public record SpecialPrice(uint Quantity, uint Value);

    public record Product(Sku Sku, UnitPrice UnitPrice, SpecialPrice? SpecialPrice);

    public interface IResult
    {
        public record Ok<T>(T Value);

        public record Fail(string Error);
    }

    public record Some<T>(T Value) : IMaybe
    {
        public static implicit operator T(Some<T> some) => some.Value;
    };

    public record None() : IMaybe;

    public interface IMaybe
    {
        public static IMaybe Of<T>(T? value) => value is null ? (IMaybe)new None() : (IMaybe)new Some<T>(value);
    }

    public record ShoppingCart(IReadOnlyCollection<Product> Products)
    {
        private ShoppingCart AddToCart(Product product)
        {
            return new ShoppingCart(Products.Append(product).ToList());
        }

        private ShoppingCart RemoveFromCart(Product product)
        {
            return new ShoppingCart(
                Products.TakeWhile(item => item != product)
                    .Concat(Products.SkipWhile(item => item != product)
                        .Skip(1)
                    ).ToList()
            );
        }

        public static ShoppingCart operator +(ShoppingCart cart, Product product) => cart.AddToCart(product);
        public static ShoppingCart operator -(ShoppingCart cart, Product product) => cart.RemoveFromCart(product);

        public int Total => Products.GroupBy(product => product)
            .Select(products => new { Product = products.Key, Count = products.Count() })
            .Select(args => CalculateTotalForProduct(args.Product, args.Count))
            .Sum();

        private static int CalculateTotalForProduct(Product product, int count)
        {
            if (product.SpecialPrice is null)
            {
                return (int)product.UnitPrice * count;
            }

            var specialQuantity = count / (int)product.SpecialPrice.Quantity;
            var regularQuantity = count % (int)product.SpecialPrice.Quantity;
            return (int)product.UnitPrice * regularQuantity + (int)product.SpecialPrice.Value * specialQuantity;
        }
    }
}