namespace Ambev.DeveloperEvaluation.Application.Sales.ListSales
{
    public class ListSalesResult
    {
        public List<SaleDto> Sales { get; set; } = new();
    }

    public class SaleDto
    {
        public Guid SaleId { get; set; } 
        public int SaleNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Customer { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountDiscont { get; set; }
        public string Branch { get; set; }
        public List<SaleItemDto> Products { get; set; } = new List<SaleItemDto>();
        public bool IsCanceled { get; set; }
    }

    public class SaleItemDto
    {
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountWithDiscount { get; set; }
    }
}
