using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

  public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Unique identifier for the sale to be updated.
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Updated sale number.
    /// </summary>
    public int SaleNumber { get; set; }

    /// <summary>
    /// Date when the sale was originally created (can be left unchanged).
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Date when the sale was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// Updated customer information.
    /// </summary>
    public string Customer { get; set; }

    /// <summary>
    /// Updated total amount of the sale.
    /// </summary>
    public decimal TotalAmount { get; set; }

    public decimal TotalAmountDiscont { get; set; }

    /// <summary>
    /// Updated branch where the sale was made.
    /// </summary>
    public string Branch { get; set; }

    /// <summary>
    /// Updated list of products in the sale.
    /// </summary>
    public List<SaleItem> Products { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Indicates whether the sale is canceled.
    /// </summary>
    public bool IsCanceled { get; set; }

    public ValidationResultDetail Validate()
    {
        var validator = new UpdateSaleCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}