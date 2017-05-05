using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    public class SaleItem
    {
        /// <summary> 
        /// Gets or sets the product identifier for the product being sold. 
        /// </summary> 
        /// <value> 
        /// The product identifier. 
        /// </value> 
        public int ProductId { get; set; }
        /// <summary> 
        /// Gets or sets the description for the sale item. 
        /// </summary>  			/// <value> 
        /// The description. 
        /// </value> 
        public string Description { get; set; }
        /// <summary> 
        /// Gets or sets the quantity for the sale item -  	/// i.e. how many units of the product are being sold. 
        /// </summary>  			/// <value> 
        /// The quantity.  			/// </value> 
        public int Quantity { get; set; }
        /// <summary> 
        /// Gets or sets the unit price for the sale item. This represents the  	/// undiscounted selling price for the product at the time of sale. 
        /// </summary>  			/// <value> 
        /// The unit price. 
        /// </value> 
        public decimal UnitPrice { get; set; }
        /// <summary> 
        /// Gets or sets the discount for the sale item. This is applied at the line 
        /// item level and not at the unit level 
        /// </summary>  			/// <value> 
        /// The discount. 
        /// </value> 
        public decimal Discount { get; set; }
        /// <summary> 
        /// Gets the total price for the sale item. This is calculated by 
        /// (Quantity * UnitPrice) - Discount 
        /// </summary>  			/// <value> 
        /// The total price.  		/// </value> 
        public decimal TotalPrice
        {
            get
            {
                return (this.Quantity * this.UnitPrice) - this.Discount;
            }
        }
    } 
}
