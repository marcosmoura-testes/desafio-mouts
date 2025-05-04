using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class ListSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ListSalesHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListSaleHandlerTests"/> class.
        /// </summary>
        public ListSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _mapper = Substitute.For<IMapper>();
            _handler = new ListSalesHandler(_saleRepository, _mapper);
        }

        /// <summary>
        /// Tests that the Handle method returns a success response when provided with a valid request.
        /// </summary>
        [Fact(DisplayName = "Handle_ValidRequest_ReturnsSuccessResponse")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new ListSalesQuery();

            var sales = SaleTestData.GenerateValidSales(1);

            var result = new ListSalesResult
            {
                Sales = _mapper.Map<List<SaleDto>>(sales),
            };

            _saleRepository.GetAllSalesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(sales);

            // Act
            var createUserResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            createUserResult.Should().NotBeNull();
            await _saleRepository.Received(1).GetAllSalesAsync(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<CancellationToken>());
        }
    }
}