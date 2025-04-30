using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(sale => sale.Products).NotEmpty().WithMessage("The sale must contain at least one product.");

            RuleForEach(sale => sale.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.Quantity).GreaterThan(0).WithMessage("Product quantity must be greater than zero.");
                product.RuleFor(p => p.Quantity).LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
            });

            RuleFor(sale => sale.Discount).Must((sale, discount) =>
            {
                int totalQuantity = sale.Products.Sum(p => p.Quantity);
                if (totalQuantity < 4) return discount == 0;
                if (totalQuantity >= 4 && totalQuantity < 10) return discount == 0.1m;
                if (totalQuantity >= 10 && totalQuantity <= 20) return discount == 0.2m;
                return false;
            }).WithMessage("Invalid discount for the total quantity of products.");
        }
    }
}
