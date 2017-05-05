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

            

            return results;
        }

        private void Apply(decimal discount, List<SaleItem> input)
        {
            foreach (var item in input)
            {

            }
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

        private decimal GetSaleTotal(List<SaleItem> saleItems)
        {
            return saleItems.Sum(item => item.UnitPrice * item.Quantity);
        }
    }
}
