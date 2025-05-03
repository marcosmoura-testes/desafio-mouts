using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.DeleteSale
{
    public class DeleteSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for DeleteSale feature
        /// </summary>
        public DeleteSaleProfile()
        {
            CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
            CreateMap<DeleteSaleResult, DeleteSaleResponse>();
        }
    }
}