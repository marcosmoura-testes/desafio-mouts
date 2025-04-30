using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    /// <summary>  
    /// Unit tests for the <see cref="Sale"/> entity.  
    /// </summary>  
    public class SaleTests
    {
        /// <summary>  
        /// Verifies that a new <see cref="Sale"/> instance initializes with default values.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldInitializeWithDefaultValues()
        {
            var sale = new Sale();

            Assert.NotNull(sale.Products);
            Assert.Empty(sale.Products);
            Assert.False(sale.IsCanceled);
            Assert.Equal(0, sale.SaleNumber);
            Assert.Equal(default, sale.SaleDate);
            Assert.Null(sale.Customer);
            Assert.Equal(0, sale.TotalAmount);
            Assert.Null(sale.Branch);
            Assert.Equal(0, sale.Discount);
        }

        /// <summary>  
        /// Validates that the properties of a <see cref="Sale"/> instance are set correctly.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldSetPropertiesCorrectly()
        {
            var sale = SaleTestData.GenerateValidSale();

            Assert.NotEqual(0, sale.SaleNumber);
            Assert.NotEqual(default, sale.SaleDate);
            Assert.False(string.IsNullOrWhiteSpace(sale.Customer));
            Assert.True(sale.TotalAmount > 0);
            Assert.False(string.IsNullOrWhiteSpace(sale.Branch));
            Assert.NotNull(sale.Products);
            Assert.All(sale.Products, product =>
            {
                Assert.False(string.IsNullOrWhiteSpace(product.Product));
                Assert.True(product.Quantity > 0);
                Assert.True(product.UnitPrice > 0);
            });
            Assert.True(sale.Discount >= 0);
        }

        /// <summary>  
        /// Tests the cancellation behavior of a <see cref="Sale"/> instance.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldHandleCancellation()
        {
            var sale = SaleTestData.GenerateValidSale();

            sale.IsCanceled = true;

            Assert.True(sale.IsCanceled);
        }

        /// <summary>  
        /// Ensures that an invalid <see cref="Sale"/> instance is generated correctly.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldGenerateInvalidSale()
        {
            var invalidSale = SaleTestData.GenerateInvalidSale();

            Assert.Equal(-1, invalidSale.SaleNumber);
            Assert.Equal(DateTime.MinValue, invalidSale.SaleDate);
            Assert.True(string.IsNullOrWhiteSpace(invalidSale.Customer));
            Assert.True(invalidSale.TotalAmount < 0);
            Assert.True(string.IsNullOrWhiteSpace(invalidSale.Branch));
            Assert.Empty(invalidSale.Products);
            Assert.False(invalidSale.IsCanceled);
        }

        /// <summary>  
        /// Verifies that the total amount of a <see cref="Sale"/> instance is calculated correctly.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldCalculateTotalAmountCorrectly()
        {
            var sale = new Sale
            {
                Products = new List<SaleItem>
                   {
                       new SaleItem { Product = "Product1", Quantity = 2, UnitPrice = 50 },
                       new SaleItem { Product = "Product2", Quantity = 1, UnitPrice = 100 }
                   },
                Discount = 20
            };

            sale.TotalAmount = sale.Products.Sum(p => p.Quantity * p.UnitPrice) - sale.Discount;

            Assert.Equal(180, sale.TotalAmount);
        }

        /// <summary>  
        /// Tests the behavior of a <see cref="Sale"/> instance when the products list is empty.  
        /// </summary>  
        [Fact]
        public void Sale_ShouldHandleEmptyProductsList()
        {
            var sale = new Sale
            {
                Products = new List<SaleItem>(),
                Discount = 10
            };

            sale.TotalAmount = sale.Products.Sum(p => p.Quantity * p.UnitPrice) - sale.Discount;

            Assert.Equal(-10, sale.TotalAmount);
        }
    }
}
