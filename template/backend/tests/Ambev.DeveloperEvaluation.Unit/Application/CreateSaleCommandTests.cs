using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    /// <summary>  
    /// Unit tests for the CreateSaleCommand class.  
    /// </summary>  
    public class CreateSaleCommandTests
    {
        /// <summary>  
        /// Validates that a valid CreateSaleCommand returns a valid result with no errors.  
        /// </summary>  
        [Fact]
        public void Validate_ValidCommand_ShouldReturnValidResult()
        {
            var command = CreateSaleCommandTestData.ValidCommand();

            var validationResult = command.Validate();

            Assert.True(validationResult.IsValid);
            Assert.Empty(validationResult.Errors);
        }

        /// <summary>  
        /// Validates that an invalid CreateSaleCommand returns an invalid result with errors.  
        /// </summary>  
        [Fact]
        public void Validate_InvalidCommand_ShouldReturnInvalidResult()
        {
            var command = CreateSaleCommandTestData.InvalidCommand();

            var validationResult = command.Validate();

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }

        /// <summary>  
        /// Validates that a valid CreateSaleCommand contains the correct data.  
        /// </summary>  
        [Fact]
        public void Validate_ValidCommand_ShouldContainCorrectData()
        {
            var command = CreateSaleCommandTestData.ValidCommand();

            var validationResult = command.Validate();

            Assert.Equal(12345, command.SaleNumber);
            Assert.Equal("John Doe", command.Customer);
            Assert.Equal(150.75m, command.TotalAmount);
            Assert.Equal("Main Branch", command.Branch);
            Assert.False(command.IsCanceled);
            Assert.Equal(2, command.Products.Count);
        }

        /// <summary>  
        /// Validates that an invalid CreateSaleCommand contains errors in the validation result.  
        /// </summary>  
        [Fact]
        public void Validate_InvalidCommand_ShouldContainErrors()
        {
            var command = CreateSaleCommandTestData.InvalidCommand();

            var validationResult = command.Validate();

            Assert.False(validationResult.IsValid);
            Assert.NotEmpty(validationResult.Errors);
        }
    }
}
