using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sale.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSale feature
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}