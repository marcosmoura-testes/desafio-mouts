using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            // Verifica se a venda existe  
            var existingSale = await _saleRepository.GetByIdAsync(command.SaleId);
            if (existingSale == null)
                throw new KeyNotFoundException($"Sale with ID {command.SaleId} not found");

            // Atualiza os campos  
            existingSale.SaleNumber = command.SaleNumber;
            existingSale.Customer = command.Customer;
            existingSale.TotalAmount = command.TotalAmount;
            existingSale.TotalAmountDiscont = command.TotalAmountDiscont;
            existingSale.Branch = command.Branch;
            existingSale.CreatedAt = command.CreatedAt;
            existingSale.UpdatedAt = command.UpdatedAt;
            existingSale.IsCanceled = command.IsCanceled;

            // Atualiza a lista de produtos  
            var updatedProducts = new List<SaleItem>();

            // Identifica itens cancelados e mantém os novos ou modificados  
            foreach (var existingItem in existingSale.Products)
            {
                var matchingItem = command.Products.FirstOrDefault(newItem => newItem.Product == existingItem.Product);
                if (matchingItem == null)
                {
                    // Item foi removido, marca como cancelado  
                    existingItem.Quantity = 0;
                    updatedProducts.Add(existingItem);
                }
                else
                {
                    // Item foi modificado, mantém as alterações  
                    updatedProducts.Add(matchingItem);
                }
            }

            // Adiciona novos itens que não estavam na lista original  
            var newItems = command.Products.Where(newItem => !existingSale.Products.Any(existingItem => existingItem.Product == newItem.Product));
            updatedProducts.AddRange(newItems);

            existingSale.UpdatedAt = DateTime.UtcNow;
            existingSale.Products = updatedProducts;

            await _saleRepository.UpdateAsync(existingSale);

            return new UpdateSaleResult
            {
                SaleId = existingSale.SaleId,
                SaleNumber = existingSale.SaleNumber,
                Success = true,
                Message = "Sale updated successfully."
            };
        }
    }
}