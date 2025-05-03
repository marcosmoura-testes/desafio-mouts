using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResponse>
{
    private readonly ISaleRepository _saleRepository;

    public DeleteSaleHandler(
        ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;
    }

    public async Task<DeleteSaleResponse> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _saleRepository.DeleteAsync(request.SaleNumber, cancellationToken);
        if (!success)
            throw new KeyNotFoundException($"Sale with Number {request.SaleNumber} not found");

        return new DeleteSaleResponse { Success = true };
    }
}


