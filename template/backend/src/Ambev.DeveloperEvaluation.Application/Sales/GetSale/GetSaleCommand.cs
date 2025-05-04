using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    public Guid SaleId { get; }

    public GetSaleCommand(Guid saleId)
    {
        SaleId = saleId;
    }
}

