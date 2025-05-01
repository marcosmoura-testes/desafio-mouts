using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesHandler : IRequestHandler<ListSalesQuery, ListSalesResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public ListSalesHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<ListSalesResult> Handle(ListSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllSalesAsync(cancellationToken);

            var salesResult = new ListSalesResult
            {
                Sales = _mapper.Map<List<SaleDto>>(sales)
            };

            return salesResult;
        }
    }

    
}
