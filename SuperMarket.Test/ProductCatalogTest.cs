using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarket.Test;

[TestClass]
public class UnitTest1
{
    private IProductCatalog Catalog;
    
    [TestMethod]
    public void TestMethod1()
    {
        var sku = new Sku("A");
        var actual = Catalog.RetrievePrice(sku);
        var expected = new UnitPrice(15);
        
        Assert.AreEqual(expected, actual);
    }
}