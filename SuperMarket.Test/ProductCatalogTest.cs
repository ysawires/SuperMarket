using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarket.Test;

[TestClass]
public class UnitTest1
{
    private IProductCatalog? _catalog;

    [TestInitialize]
    public void Setup()
    {
        _catalog = new ProductCatalog();
    }
    
    [TestMethod]
    public void RetrieveUnitPrice()
    {
        var sku = new Sku("A");
        var actual = _catalog!.FindOneBySku(sku);
        var expected = new Product(sku, new UnitPrice(50), new SpecialPrice(3, 130));
        if (actual is Some<Product> product)
        {
            Assert.AreEqual(expected, product);
        }
        
    }
}