using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;

public class DeleteSaleCommand : IRequest<DeleteSaleResponse>
{
    public Guid SaleId { get; set; }

    public DeleteSaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }
}

