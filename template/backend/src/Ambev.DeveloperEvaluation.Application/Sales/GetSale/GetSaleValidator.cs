using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleValidator : AbstractValidator<GetSaleCommand>
{
    public GetSaleValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale Id is required");
    }
}
