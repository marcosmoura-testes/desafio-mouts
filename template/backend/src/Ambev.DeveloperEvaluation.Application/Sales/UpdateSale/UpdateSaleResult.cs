namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public int Id { get; set; }
        public Guid SaleNumber { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
