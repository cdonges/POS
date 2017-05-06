using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POSLibrary.BusinessEntities;
using POSLibrary.Interfaces;
using POSLibrary.Models;

namespace POS
{
    /// <summary>
    /// Program to demonstrate discount functionality
    /// </summary>
    class Program
    {
        /// <summary>
        /// Demo sale items
        /// </summary>
        static private List<SaleItem> _saleItems = new List<SaleItem>() { 
            new SaleItem() { ProductId = 1, Description = "Widget", Quantity = 1, UnitPrice = 1.75M, Discount = 0M },
            new SaleItem() { ProductId = 2, Description = "Gadget", Quantity = 2, UnitPrice = 2.95M, Discount = 0M },
            new SaleItem() { ProductId = 3, Description = "Gizmo", Quantity = 3, UnitPrice = 2.35M, Discount = 0M }
        };

        /// <summary>
        /// Demo products
        /// </summary>
        static private List<Product> _products = new List<Product> {
                new Product() { ProductId = 1, MinimumPrice = 0M },
                new Product() { ProductId = 2, MinimumPrice = 0M },
                new Product() { ProductId = 3, MinimumPrice = 0M }
            };

        /// <summary>
        /// Sale business entity
        /// </summary>
        static ISale applyDiscount = new SaleBusinessEntity();

        /// <summary>
        /// Main program
        /// </summary>
        /// <param name="args">arguments</param>
        static void Main(string[] args)
        {
            decimal salePrice;

            Console.WriteLine("Please enter sale price");
            string salePriceString = Console.ReadLine();

            if (decimal.TryParse(salePriceString, out salePrice))
            {
                var result = applyDiscount.ApplyDiscount(salePrice, _saleItems, _products);
                PrintSaleItems(result);
            }
            else
            {
                Console.WriteLine("'" + salePrice + "' is not a valid sale price");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Print out sale items
        /// </summary>
        /// <param name="saleItems">sale items to print</param>
        private static void PrintSaleItems(IEnumerable<SaleItem> saleItems)
        {
            Console.WriteLine("Description\tQuantity\tUnitPrice\tDiscount\tTotalPrice");

            foreach (var saleItem in saleItems)
            {
                Console.WriteLine(saleItem.Description + "\t" + saleItem.Quantity + "\t" + saleItem.UnitPrice + "\t" + saleItem.Discount + "\t" + saleItem.TotalPrice);
            }
        }
    }
}
