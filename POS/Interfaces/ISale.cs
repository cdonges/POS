using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Models;

namespace POS.Interfaces
{
    public interface ISale
    {
        List<SaleItem> ApplyDiscount(decimal salePrice, List<SaleItem> saleItems, List<Product> prodcuts);
    }
}
