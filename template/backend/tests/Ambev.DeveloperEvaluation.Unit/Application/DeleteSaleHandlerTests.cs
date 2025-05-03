using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly DeleteSaleHandler _handler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteSaleHandlerTests"/> class.
        /// </summary>
        public DeleteSaleHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _handler = new DeleteSaleHandler(_saleRepository);
        }

        /// <summary>
        /// Tests that the Handle method returns a success response when provided with a valid request.
        /// </summary>
        [Fact(DisplayName = "Handle_ValidRequest_ReturnsSuccessResponse")]
        public async Task Handle_ValidRequest_ReturnsSuccessResponse()
        {
            // Arrange
            var command = new DeleteSaleCommand(Guid.NewGuid());

            _saleRepository.DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(true);

            // Act
            var createUserResult = await _handler.Handle(command, CancellationToken.None);

            // Assert
            createUserResult.Should().NotBeNull();
            await _saleRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        }
    }
}
