using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

public static class CreateSaleHandlerTestData
{
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

    private static readonly Faker<CreateSaleCommand> createSaleCommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(s => s.Customer, f => f.Person.FullName)
        .RuleFor(s => s.Branch, f => f.Company.CompanyName());

    public static CreateSaleCommand GenerateValidCommand()
    {
        var sale = createSaleCommandFaker.Generate();
        sale.Products = GenerateSaleItemsForValidation(2, true);
        sale.TotalAmount = sale.Products.Sum(p => p.TotalAmount);
        sale.TotalAmountDiscont = sale.Products.Sum(p => p.TotalAmountWithDiscount);
        return sale;
    }

    public static CreateSaleCommand GenerateInvalidCommand()
    {
        var command = createSaleCommandFaker.Generate();
        command.Products = GenerateSaleItemsForValidation(2, false);
        return command;
    }
}

