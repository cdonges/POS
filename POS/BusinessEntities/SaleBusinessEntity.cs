using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Models;
using POS.Interfaces;

namespace POS.BusinessEntities
{
    public class SaleBusinessEntity : ISale
    {
        private Dictionary<int, decimal> minimumPrices;

        public List<SaleItem> ApplyDiscount(decimal salePrice, List<SaleItem> saleItems, List<Product> products)
        {
            // set up a dictionary of minimum prices
            minimumPrices = products.ToDictionary(product => product.ProductId, product => product.MinimumPrice);

            // copy the sale items to return from the function. Don't want to modify what's passed in
            var results = CloneSaleItems(saleItems);

            var total = GetSaleTotal(results);

            var totalDiscount = GetSaleTotal(results) - salePrice;
            var remainingDiscount = totalDiscount;

            IEnumerable<SaleItem> remainingItems = results;

            do
            {
                foreach (var item in remainingItems)
                {
                    // Item Quantity * Item Unit Price * Total Discount/Undiscounted Sale Total 
                    var itemDiscount = item.Quantity * item.UnitPrice * totalDiscount / total;
                    var linePrice = Math.Max(item.TotalPrice - itemDiscount, minimumPrices[item.ProductId] * item.Quantity);
                    var appliedDiscount = Math.Round(item.TotalPrice - linePrice, 2);
                    item.Discount += appliedDiscount;
                    remainingDiscount -= appliedDiscount;
                }

                remainingItems = remainingItems.Where(item => item.TotalPrice > minimumPrices[item.ProductId] * item.Quantity);
                total = GetSaleTotal(remainingItems);
                totalDiscount = remainingDiscount;
            } while (remainingItems.Any() && remainingDiscount > 0.10M);

            if (remainingDiscount > 0 && remainingItems.Any())
            {
                GetLargestPrice(results).Discount += remainingDiscount;
            }

            return results;
        }

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

        private decimal GetSaleTotal(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.Sum(item => item.UnitPrice * item.Quantity);
        }

        private decimal GetDiscountTotal(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.Sum(item => item.Discount);
        }

        private SaleItem GetLargestPrice(IEnumerable<SaleItem> saleItems)
        {
            return saleItems.OrderByDescending(item => item.UnitPrice).First();
        }
    }
}
