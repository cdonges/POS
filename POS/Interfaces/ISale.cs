using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Models;

namespace POS.Interfaces
{
    /// <summary>
    /// Sale business object interface
    /// </summary>
    public interface ISale
    {
        /// <summary>
        /// Apply discount to sale items
        /// </summary>
        /// <param name="salePrice">discounted sale price</param>
        /// <param name="saleItems">list of sale items</param>
        /// <param name="products">list of products</param>
        /// <returns>new list of sale items with discount applied</returns>
        List<SaleItem> ApplyDiscount(decimal salePrice, List<SaleItem> saleItems, List<Product> prodcuts);
    }
}
