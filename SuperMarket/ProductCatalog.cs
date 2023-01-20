namespace SuperMarket;

public interface IProductCatalog
{
    UnitPrice RetrievePrice(Sku sku);
};

public record Sku(string Identifier);

public record UnitPrice(uint Value);
