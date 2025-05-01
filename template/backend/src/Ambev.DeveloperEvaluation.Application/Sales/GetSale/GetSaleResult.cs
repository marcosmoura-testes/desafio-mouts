using Ambev.DeveloperEvaluation.Domain.Entities;

public class GetSaleResult
{
    /// <summary>  
    /// Unique identifier number for the sale.  
    /// </summary>  
    public int SaleNumber { get; set; }

    /// <summary>  
    /// Date when the sale was made.  
    /// </summary>  
    public DateTime CreatedAt { get; set; }

    /// <summary>  
    /// Date when the sale was updated.  
    /// </summary>  
    public DateTime UpdatedAt { get; set; }

    /// <summary>  
    /// Name or identifier of the customer.  
    /// </summary>  
    public string Customer { get; set; }

    /// <summary>  
    /// Total value of the sale.  
    /// </summary>  
    public decimal TotalAmount { get; set; }

    public decimal TotalAmountDiscont { get; set; }

    /// <summary>  
    /// Branch where the sale was conducted.  
    /// </summary>  
    public string Branch { get; set; }

    /// <summary>  
    /// List of sold items.  
    /// </summary>  
    public List<SaleItem> Products { get; set; } = new List<SaleItem>();

    /// <summary>  
    /// Indicates whether the sale was canceled.  
    /// </summary>  
    public bool IsCanceled { get; set; }
}

