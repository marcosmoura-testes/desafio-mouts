using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>  
    /// Provides methods for generating test data for the Sale entity using the Bogus library.  
    /// </summary>  
    internal static class SaleTestData
    {
        /// <summary>  
        /// Generates a list of SaleItems to simulate each validation rule in CreateSaleCommandValidator.  
        /// </summary>  
        /// <param name="quantityProduct">The quantity of the product to generate.</param>  
        /// <param name="isValid">Indicates whether to generate valid or invalid SaleItems.</param>  
        /// <returns>A list of SaleItems covering all validation scenarios.</returns>  
        private static List<SaleItem> GenerateSaleItemsForValidation(int quantityProduct = 1, bool isValid = true)
        {
            if (isValid)
            {
                decimal CalculateDiscount(int quantity)
                {
                    if (quantity < 4) return 0;
                    if (quantity >= 4 && quantity < 10) return 10;
                    if (quantity >= 10 && quantity <= 20) return 20;
                    return 0;
                }

                decimal unitPrice = 10;
                decimal discountPercentage = CalculateDiscount(quantityProduct);
                decimal totalAmount = unitPrice * quantityProduct;
                decimal discountAmount = totalAmount * (discountPercentage / 100);
                decimal totalAmountWithDiscount = totalAmount - discountAmount;

                return new List<SaleItem>
               {
                   new SaleItem
                   {
                       Product = "Valid Product",
                       Quantity = quantityProduct,
                       UnitPrice = unitPrice,
                       Discount = discountPercentage,
                   }
               };
            }
            else
            {
                return new List<SaleItem>
               {
                   new SaleItem
                   {
                       Product = "InvalidProduct1",
                       Quantity = 0,
                       UnitPrice = 50,
                       Discount = 0
                   }
               };
            }
        }

        private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
           .RuleFor(s => s.CreatedAt, f => f.Date.Past(1))
           .RuleFor(s => s.Customer, f => f.Person.FullName)
           .RuleFor(s => s.Branch, f => f.Company.CompanyName())
           .RuleFor(s => s.IsCanceled, f => f.Random.Bool());

        /// <summary>  
        /// Generates a valid Sale entity with randomized data.  
        /// </summary>  
        /// <returns>A valid Sale entity.</returns>  
        public static Sale GenerateValidSale()
        {
            var sale = SaleFaker.Generate();
            sale.Products = GenerateSaleItemsForValidation(2, true);
            sale.TotalAmount = sale.Products.Sum(p => p.TotalAmount);
            sale.TotalAmountDiscont = sale.Products.Sum(p => p.TotalAmountWithDiscount);
            return sale;
        }

        /// <summary>  
        /// Generates a invalid Sale entity with randomized data.  
        /// </summary>  
        /// <returns>A valid Sale entity.</returns>  
        public static Sale GenerateInvalidSale()
        {
            var sale = SaleFaker.Generate();
            sale.Products = GenerateSaleItemsForValidation(2, false);
            return sale;
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
    }
}
