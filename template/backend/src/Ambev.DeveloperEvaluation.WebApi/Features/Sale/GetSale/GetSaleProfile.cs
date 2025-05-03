using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.GetSale
{
    public class GetSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for GetSale feature
        /// </summary>
        public GetSaleProfile()
        {
            CreateMap<GetSaleResult, GetSaleResponse>();
        }
    }
}