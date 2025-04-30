using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    internal static class CreateSaleCommandTestData
    {
        public static CreateSaleCommand ValidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = 12345,
                SaleDate = DateTime.UtcNow,
                Customer = "John Doe",
                TotalAmount = 150.75m,
                Branch = "Main Branch",
                Products = new List<SaleItem>
                   {
                       new SaleItem
                       {
                           Product = "Product A",
                           Quantity = 2,
                           UnitPrice = 50.00m,
                       },
                       new SaleItem
                       {
                           Product = "Product B",
                           Quantity = 1,
                           UnitPrice = 60.75m,
                       }
                   },
                IsCanceled = false
            };
        }

        public static CreateSaleCommand InvalidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = 0,
                SaleDate = DateTime.MinValue,
                Customer = string.Empty,
                TotalAmount = -10.00m,
                Branch = string.Empty,
                Products = new List<SaleItem>(),
                IsCanceled = false
            };
        }
    }
}
