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
        /// Validates that a <see cref="Sale"/> object initialized with valid values passes validation.
        /// </summary>
        [Fact(DisplayName = "Sale should initialize with valid values")]
        public void Sale_ShouldInitializeWithValidValues()
        {
            var sale = SaleTestData.GenerateValidSale();
            var result = sale.Validate();
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        /// <summary>
        /// Validates that a <see cref="Sale"/> object initialized with invalid values fails validation.
        /// </summary>
        [Fact(DisplayName = "Sale should initialize with invalid values")]
        public void Sale_ShouldInitializeWithInvalidValues()
        {
            var sale = SaleTestData.GenerateInvalidSale();
            var result = sale.Validate();
            Assert.False(result.IsValid);
            Assert.NotEmpty(result.Errors);
        }
    }
}
