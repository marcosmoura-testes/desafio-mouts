using Ambev.DeveloperEvaluation.Common.Validation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        /// <summary>  
        /// Unique identifier number for the sale.  
        /// </summary>  
        public Guid SaleNumber { get; set; }

        /// <summary>  
        /// Date when the sale was made.  
        /// </summary>  
        public DateTime CreatedAt { get; set; }

        /// <summary>  
        /// Date when the sale was updated.  
        /// </summary>  
        public DateTime UpdatedAt { get; set; }

        /// <summary>  
        /// Name or identifier of the customer.  
        /// </summary>  
        public string Customer { get; set; }

        /// <summary>  
        /// Total value of the sale.  
        /// </summary>  
        public decimal TotalAmount { get; set; }

        public decimal TotalAmountDiscont { get; set; }

        /// <summary>  
        /// Branch where the sale was conducted.  
        /// </summary>  
        public string Branch { get; set; }

        /// <summary>  
        /// List of sold items.  
        /// </summary>  
        public List<SaleItem> Products { get; set; } = new List<SaleItem>();

        /// <summary>  
        /// Indicates whether the sale was canceled.  
        /// </summary>  
        public bool IsCanceled { get; set; }

        public Sale()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }

    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(sale => sale.Customer).NotEmpty().WithMessage("The customer name cannot be empty.");
            RuleFor(sale => sale.Branch).NotEmpty().WithMessage("The branch name cannot be empty.");
            RuleFor(sale => sale.Products).NotEmpty().WithMessage("The sale must contain at least one product.");
            RuleFor(sale => sale.Products).Must(products => products.All(p => p.Quantity > 0)).WithMessage("Product quantity must be greater than zero.");
            RuleFor(sale => sale.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(sale => sale.TotalAmount).Must((sale, totalAmount) =>
            {
                var totalProductAmount = sale.Products.Sum(p => p.TotalAmount);
                return totalAmount == totalProductAmount;
            }).WithMessage("Total amount must match the sum of all product amounts without applying discounts.");

            RuleFor(sale => sale.TotalAmountDiscont).Must((sale, totalAmountDiscont) =>
            {
                var totalProductAmountWithDiscount = sale.Products.Sum(p => p.TotalAmountWithDiscount);
                return totalAmountDiscont == totalProductAmountWithDiscount;
            }).WithMessage("Total amount with discount must match the sum of all product amounts with discounts applied.");

            RuleForEach(sale => sale.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Product quantity must be greater than zero.");
                product.RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
                product.RuleFor(p => p.Discount).Must((product, discount) =>
                {
                    if (product.Quantity < 4) return discount == 0;
                    if (product.Quantity >= 4 && product.Quantity < 10) return discount == 10;
                    if (product.Quantity >= 10 && product.Quantity <= 20) return discount == 20;
                    return false;
                }).WithMessage("Invalid discount for the product quantity.");
            });
        }
    }
}
