﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

using POSLibrary.Interfaces;
using POSLibrary.BusinessEntities;
using POSLibrary.Models;

namespace POSTest
{
    /// <summary>
    /// Tests based on requirments
    /// </summary>
    [TestClass]
    public class ApplyDiscountTest
    {
        /// <summary>
        /// list of sale items
        /// </summary>
        private List<SaleItem> _saleItems = new List<SaleItem>() { 
            new SaleItem() { ProductId = 1, Description = "Widget", Quantity = 1, UnitPrice = 1.75M, Discount = 0M },
            new SaleItem() { ProductId = 2, Description = "Gadget", Quantity = 2, UnitPrice = 2.95M, Discount = 0M },
            new SaleItem() { ProductId = 3, Description = "Gizmo", Quantity = 3, UnitPrice = 2.35M, Discount = 0M }
        };

        /// <summary>
        /// Simple case - no minimums and discount can be allocated
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            decimal salePrice = 10M;

            ISale applyDiscount = new SaleBusinessEntity();

            var products = new List<Product> {
                new Product() { ProductId = 1, MinimumPrice = 0M },
                new Product() { ProductId = 2, MinimumPrice = 0M },
                new Product() { ProductId = 3, MinimumPrice = 0M }
            };

            var result = applyDiscount.ApplyDiscount(salePrice, _saleItems, products);

            var itemDictionary = result.ToDictionary(item => item.ProductId, item => item.TotalPrice);

            Assert.AreEqual(itemDictionary[1], 1.19M);
            Assert.AreEqual(itemDictionary[2], 4.01M);
            Assert.AreEqual(itemDictionary[3], 4.80M);
        }

        /// <summary>
        /// One product with a minimum price
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            decimal salePrice = 10M;

            ISale applyDiscount = new SaleBusinessEntity();

            var products = new List<Product> {
                new Product() { ProductId = 1, MinimumPrice = 0M },
                new Product() { ProductId = 2, MinimumPrice = 2.50M },
                new Product() { ProductId = 3, MinimumPrice = 0M }
            };

            var result = applyDiscount.ApplyDiscount(salePrice, _saleItems, products);

            var itemDictionary = result.ToDictionary(item => item.ProductId, item => item.TotalPrice);

            Assert.AreEqual(itemDictionary[1], 0.99M);
            Assert.AreEqual(itemDictionary[2], 5.00M);
            Assert.AreEqual(itemDictionary[3], 4.01M);
        }

        /// <summary>
        /// Total discount cannot be applied
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {
            decimal salePrice = 10.23M;

            ISale applyDiscount = new SaleBusinessEntity();

            var products = new List<Product> {
                new Product() { ProductId = 1, MinimumPrice = 0M },
                new Product() { ProductId = 2, MinimumPrice = 0M },
                new Product() { ProductId = 3, MinimumPrice = 0M }
            };

            var result = applyDiscount.ApplyDiscount(salePrice, _saleItems, products);

            var itemDictionary = result.ToDictionary(item => item.ProductId, item => item.TotalPrice);

            Assert.AreEqual(itemDictionary[1], 1.22M);
            Assert.AreEqual(itemDictionary[2], 4.11M);
            Assert.AreEqual(itemDictionary[3], 4.90M);
        }

        /// <summary>
        /// There is a rounding error that needs to be applied
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            decimal salePrice = 10M;

            ISale applyDiscount = new SaleBusinessEntity();

            var products = new List<Product> {
                new Product() { ProductId = 1, MinimumPrice = 1.50M },
                new Product() { ProductId = 2, MinimumPrice = 2.50M },
                new Product() { ProductId = 3, MinimumPrice = 2.00M }
            };

            var result = applyDiscount.ApplyDiscount(salePrice, _saleItems, products);

            var itemDictionary = result.ToDictionary(item => item.ProductId, item => item.TotalPrice);

            Assert.AreEqual(itemDictionary[1], 1.50M);
            Assert.AreEqual(itemDictionary[2], 5.00M);
            Assert.AreEqual(itemDictionary[3], 6.00M);
        }
    }
}
