using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        /// <summary>  
        /// Name or description of the product.  
        /// </summary>  
        public string Product { get; set; }

        /// <summary>  
        /// Quantity of the product sold.  
        /// </summary>  
        public int Quantity { get; set; }

        /// <summary>  
        /// Unit price of the product.  
        /// </summary>  
        public decimal UnitPrice { get; set; }

        /// <summary>  
        /// Discount applied to the product (in percentage, e.g., 10 for 10%).  
        /// </summary>  
        public decimal Discount { get; set; }

        /// <summary>  
        /// Total amount before applying the discount.  
        /// </summary>  
        public decimal TotalAmount => Quantity * UnitPrice;

        /// <summary>  
        /// Total amount after applying the discount.  
        /// </summary>  
        public decimal TotalAmountWithDiscount => TotalAmount * (1 - Discount / 100);
    }
}
