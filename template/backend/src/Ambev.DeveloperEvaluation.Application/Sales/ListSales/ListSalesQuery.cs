using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesQuery : IRequest<ListSalesResult>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
