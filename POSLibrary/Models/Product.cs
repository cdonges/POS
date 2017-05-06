using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POSLibrary.Models
{
    public class Product
    {
        /// <summary> 
        /// Gets or sets the product identifier. 
        /// </summary> 
        /// <value> 
        /// The product identifier.  			/// </value> 
        public int ProductId { get; set; }
        /// <summary> 
        /// Gets or sets the minimum price for a product. A product must never  	/// be sold for a price lower than the minimum price. 
        /// </summary>  			/// <value> 
        /// The minimum price. 
        /// </value> 
        public decimal MinimumPrice { get; set; }
    } 
}
