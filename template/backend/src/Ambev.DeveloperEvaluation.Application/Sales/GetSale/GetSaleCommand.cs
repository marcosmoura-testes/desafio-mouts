using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

public class GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    public Guid SaleNumber { get; }

    public GetSaleCommand(Guid saleNumber)
    {
        SaleNumber = saleNumber;
    }
}

