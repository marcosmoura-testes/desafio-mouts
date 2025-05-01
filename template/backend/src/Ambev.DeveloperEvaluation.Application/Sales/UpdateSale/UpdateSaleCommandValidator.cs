using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    internal class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(sale => sale.SaleId).NotEmpty().WithMessage("The sale ID cannot be empty.");
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
