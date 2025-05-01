using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    public Guid SaleNumber { get; set; }

    public DeleteSaleCommand(Guid saleNumber)
    {
        SaleNumber = saleNumber;
    }
}

