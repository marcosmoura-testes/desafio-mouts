using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class UpdateSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly UpdateSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
        /// </summary>
        public UpdateSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepository);
        }

        /// <summary>
        /// Tests that the Handle method returns a success response when provided with a valid request.
        /// </summary>
        [Fact(DisplayName = "Handle_ValidRequest_ReturnsSuccessResponse")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateValidCommand();

            command.SaleId = Guid.NewGuid();

            var sale = SaleTestData.GenerateValidSale();

            var result = new UpdateSaleResult
            {
                SaleId = sale.SaleId,
                SaleNumber = sale.SaleNumber,
            };

            _mapper.Map<Sale>(command).Returns(sale);
            _mapper.Map<UpdateSaleResult>(sale).Returns(result);

            _saleRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(sale);

            // Act
            var createUserResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            createUserResult.Should().NotBeNull();
            createUserResult.SaleNumber.Should().Be(sale.SaleNumber);
            await _saleRepository.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }

        /// <summary>
        /// Tests that the Handle method throws a ValidationException when provided with an invalid request.
        /// </summary>
        [Fact(DisplayName = "Handle_InvalidRequest_ThrowsValidationException")]
        public async Task Handle_InvalidRequest_ThrowsValidationException()
        {
            // Arrange
            var command = UpdateSaleHandlerTestData.GenerateInvalidCommand();

            // Act
            var act = () => _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<FluentValidation.ValidationException>();
        }
    }
}
