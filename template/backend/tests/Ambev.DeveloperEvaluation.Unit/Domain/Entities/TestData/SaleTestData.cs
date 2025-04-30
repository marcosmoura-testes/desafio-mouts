using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>  
    /// Provides methods for generating test data for the Sale entity using the Bogus library.  
    /// </summary>  
    internal static class SaleTestData
    {
        private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
            .RuleFor(si => si.Product, f => f.Commerce.ProductName())
            .RuleFor(si => si.Quantity, f => f.Random.Int(1, 100))
            .RuleFor(si => si.UnitPrice, f => f.Finance.Amount(1, 1000));

        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
           .RuleFor(s => s.SaleNumber, f => f.Random.Int(1, 100000))
           .RuleFor(s => s.SaleDate, f => f.Date.Past(1))
           .RuleFor(s => s.Customer, f => f.Person.FullName)
           .RuleFor(s => s.Products, f => SaleItemFaker.Generate(f.Random.Int(1, 10)))
           .RuleFor(s => s.Discount, f => f.Finance.Amount(0, 500))
           .RuleFor(s => s.TotalAmount, (f, s) => s.Products.Sum(p => p.Quantity * p.UnitPrice) - s.Discount)
           .RuleFor(s => s.Branch, f => f.Company.CompanyName())
           .RuleFor(s => s.IsCanceled, f => f.Random.Bool());

        /// <summary>  
        /// Generates a valid Sale entity with randomized data.  
        /// </summary>  
        /// <returns>A valid Sale entity.</returns>  
        public static Sale GenerateValidSale()
        {
            return SaleFaker.Generate();
        }

        /// <summary>  
        /// Generates a list of valid Sale entities with randomized data.  
        /// </summary>  
        /// <param name="count">The number of Sale entities to generate.</param>  
        /// <returns>A list of valid Sale entities.</returns>  
        public static List<Sale> GenerateValidSales(int count)
        {
            return SaleFaker.Generate(count);
        }

        /// <summary>  
        /// Generates a Sale entity with invalid data for testing negative scenarios.  
        /// </summary>  
        /// <returns>An invalid Sale entity.</returns>  
        public static Sale GenerateInvalidSale()
        {
            return new Sale
            {
                SaleNumber = -1,
                SaleDate = DateTime.MinValue,
                Customer = string.Empty,
                TotalAmount = -100,
                Branch = string.Empty,
                Discount = -50,
                Products = new List<SaleItem>(),
                IsCanceled = false
            };
        }
    }
}
