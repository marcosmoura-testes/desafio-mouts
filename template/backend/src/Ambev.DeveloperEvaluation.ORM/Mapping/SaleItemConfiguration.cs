using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            // Define table name  
            builder.ToTable("SaleItems");

            // Define primary key  
            builder.HasKey(si => si.Product);

            // Map properties  
            builder.Property(si => si.Product)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(si => si.Quantity)
                .IsRequired();

            builder.Property(si => si.UnitPrice)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.Discount)
                .IsRequired()
                .HasColumnType("decimal(5,2)");

            builder.Property(si => si.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(si => si.TotalAmountWithDiscount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
