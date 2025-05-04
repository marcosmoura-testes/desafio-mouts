namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid SaleId { get; set; }
        public int SaleNumber { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
