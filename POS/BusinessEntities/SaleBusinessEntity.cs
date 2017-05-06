using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Models;
using POS.Interfaces;

namespace POS.BusinessEntities
{
    /// <summary>
    /// Handle processing of sale items
    /// </summary>
    public class SaleBusinessEntity : ISale
    {
        /// <summary>
        /// Product minimum prices
        /// </summary>
        private Dictionary<int, decimal> minimumPrices;

        /// <summary>
        /// Apply discount to sale items
        /// </summary>
        /// <param name="salePrice">discounted sale price</param>
        /// <param name="saleItems">list of sale items</param>
        /// <param name="products">list of products</param>
        /// <returns>new list of sale items with discount applied</returns>
        public List<SaleItem> ApplyDiscount(decimal salePrice, List<SaleItem> saleItems, List<Product> products)
        {
            // set up a dictionary of minimum prices
            minimumPrices = products.ToDictionary(product => product.ProductId, product => product.MinimumPrice);

            // copy the sale items to return from the function. Don't want to modify what's passed in
            var results = CloneSaleItems(saleItems);

            var total = GetSaleTotal(results);

            // work out total discount
            var totalDiscount = GetSaleTotal(results) - salePrice;
            var remainingDiscount = totalDiscount;

            IEnumerable<SaleItem> remainingItems = results;

            // loop through until all discount possible applied
            do
            {
                // loop through items applying discount
                foreach (var item in remainingItems)
                {
                    var itemDiscount = Math.Round(item.Quantity * item.UnitPrice * totalDiscount / total, 2);
                    var linePrice = Math.Max(item.TotalPrice - itemDiscount, minimumPrices[item.ProductId] * item.Quantity);
                    var appliedDiscount = item.TotalPrice - linePrice;
                    item.Discount += appliedDiscount;
                    remainingDiscount -= appliedDiscount;
                }

                // get a list of items that can have more discount applied
                remainingItems = remainingItems.Where(item => item.TotalPrice > minimumPrices[item.ProductId] * item.Quantity);
                total = GetSaleTotal(remainingItems);
                totalDiscount = remainingDiscount;
            } while (remainingItems.Any() && remainingDiscount > 0.10M);

            // apply rounding error to biggest value item
            if (remainingDiscount > 0 && remainingItems.Any())
            {
                GetLargestItem(results).Discount += remainingDiscount;
            }

            return results;
        }

        /// <summary>
        /// copy a list of sale items to a new list with new items
        /// </summary>
        /// <param name="input">list of sale items</param>
        /// <returns>list of new sale items</returns>
        private List<SaleItem> CloneSaleItems(List<SaleItem> input)
        {
            return input.Select(item => new SaleItem()
            {
                ProductId = item.ProductId,
                Discount = item.Discount,
                Description = item.Description,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList();
        }

        /// <summary>
        /// get total value fo a sale
        /// </summary>
        /// <param name="saleItems">list of sale items</param>
        /// <returns>sum of total value without discount</returns>
        private decimal GetSaleTotal(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.Sum(item => item.UnitPrice * item.Quantity);
        }

        /// <summary>
        /// Get total of discount for all items
        /// </summary>
        /// <param name="saleItems">list of sale items</param>
        /// <returns>total discount</returns>
        private decimal GetDiscountTotal(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.Sum(item => item.Discount);
        }

        /// <summary>
        /// Get largest item based on unit price x quantity
        /// </summary>
        /// <param name="saleItems">list of sale items</param>
        /// <returns>largest item object</returns>
        private SaleItem GetLargestItem(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.OrderByDescending(item => item.UnitPrice * item.Quantity).First();
        }
    }
}
