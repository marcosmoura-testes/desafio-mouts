using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {/// <summary>
     /// Unique identifier number for the sale.
     /// </summary>
        public int SaleNumber { get; set; }

        /// <summary>
        /// Date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Name or identifier of the customer.
        /// </summary>
        public string Customer { get; set; }

        /// <summary>
        /// Total value of the sale.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Branch where the sale was conducted.
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        /// Discount applied to the item.
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// List of sold items.
        /// </summary>
        public List<SaleItem> Products { get; set; } = new List<SaleItem>();

        /// <summary>
        /// Indicates whether the sale was canceled.
        /// </summary>
        public bool IsCanceled { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
