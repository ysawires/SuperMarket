namespace SuperMarket;

public interface IProductCatalog
{
    IMaybe FindOneBySku(Sku sku);
};

public class ProductCatalog : IProductCatalog
{
    // Item  Price   Offer
    // --------------------------
    // A     50       3 for 130
    // B     30       2 for 45
    // C     20
    // D     15
    public IMaybe FindOneBySku(Sku sku)
    {
        List<Product> products = new List<Product>()
        {
            new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130)),
            new Product(new Sku("B"), new UnitPrice(30), new SpecialPrice(2, 45)),
            new Product(new Sku("C"), new UnitPrice(20), null),
            new Product(new Sku("D"), new UnitPrice(15), null),
        };

        return IMaybe.Of(products.Find(product => product.Sku == sku));
    }
}

public record Sku(string Identifier);

public record UnitPrice(uint Value);
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
    public static IMaybe Of<T>(T? value) => value is null ? (IMaybe) new None() : (IMaybe) new Some<T>(value);
}