using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.List
{
    public class ListSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for ListSale feature
        /// </summary>
        public ListSaleProfile()
        {
            CreateMap<ListSaleRequest, ListSalesQuery>();
            CreateMap<ListSalesResult, ListSaleResponse>();
        }
    }
}