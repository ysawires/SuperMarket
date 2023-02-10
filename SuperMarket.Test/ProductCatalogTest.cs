using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SuperMarket.Test
{

    [TestClass]
    public class ProductCatalogTest
    {
        private IProductCatalog? _catalog;

        [TestInitialize]
        public void Setup()
        {
            _catalog = new ProductCatalog();
        }

        [TestMethod]
        public void RetrieveProduct()
        {
            var sku = new Sku("A");
            var actualMaybe = _catalog!.FindOneBySku(sku);
            var expected = new Product(sku, new UnitPrice(50), new SpecialPrice(3, 130));
            if (actualMaybe is Some<Product> actual)
            {
                Assert.AreEqual(expected, actual);
            }
            else
            {
                Assert.Fail("Product sku not found in catalog");
            }

        }

        [TestMethod]
        public void AddProductToShoppingCart()
        {
            var shoppingCart = new ShoppingCart(Array.Empty<Product>());
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var actual = shoppingCart + productA + productB;

            var expected = new ShoppingCart(new List<Product> { productA, productB });
            CollectionAssert.AreEqual(expected.Products.ToList(), actual.Products.ToList());
        }

        [TestMethod]
        public void RemoveProductFromShoppingCart()
        {
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var shoppingCart = new ShoppingCart(new List<Product> { productA, productB });
            var actual = shoppingCart - productB;

            var expected = new ShoppingCart(new List<Product> { productA });
            CollectionAssert.AreEqual(expected.Products.ToList(), actual.Products.ToList());
        }

        [TestMethod]
        public void RemoveProductFromNotEmptyCartWithoutProduct()
        {
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var shoppingCart = new ShoppingCart(new List<Product> { productA, productA });
            var actual = shoppingCart - productB;

            var expected = new ShoppingCart(new List<Product> { productA, productA });
            CollectionAssert.AreEqual(expected.Products.ToList(), actual.Products.ToList());
        }

        [TestMethod]
        public void RemoveProductFromEmptyCart()
        {
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var shoppingCart = new ShoppingCart(new List<Product> { });
            var actual = shoppingCart - productB;

            var expected = new ShoppingCart(new List<Product> { });
            CollectionAssert.AreEqual(expected.Products.ToList(), actual.Products.ToList());
        }

        [TestMethod]
        public void GetTotal()
        {
            var shoppingCart = new ShoppingCart(Array.Empty<Product>());
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var actual = shoppingCart + productA + productB;

            const int expected = 70;
            Assert.AreEqual(expected, actual.Total);
        }

        [TestMethod]
        public void GetTotalWithSpecialPrice()
        {
            var shoppingCart = new ShoppingCart(Array.Empty<Product>());
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));

            var actual = shoppingCart + productA + productA + productA;

            const int expected = 130;
            Assert.AreEqual(expected, actual.Total);
        }

        [TestMethod]
        public void GetTotalWithSpecialPriceAndUnitPrice()
        {
            var shoppingCart = new ShoppingCart(Array.Empty<Product>());
            var productA = new Product(new Sku("A"), new UnitPrice(50), new SpecialPrice(3, 130));
            var productB = new Product(new Sku("B"), new UnitPrice(20), null);

            var actual = shoppingCart + productA + productA + productA + productA + productB + productB;

            const int expected = 220;
            Assert.AreEqual(expected, actual.Total);
        }
    }
}