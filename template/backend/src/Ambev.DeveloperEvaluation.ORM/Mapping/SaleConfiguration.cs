using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            // Table name  
            builder.ToTable("Sales");

            // Primary key  
            builder.HasKey(s => s.SaleId);

            // Properties  
            builder.Property(s => s.SaleId)
                .IsRequired();

            builder.Property(s => s.SaleNumber)
                .IsRequired();

            builder.Property(s => s.CreatedAt)
                .IsRequired();

            builder.Property(s => s.UpdatedAt)
                .IsRequired();

            builder.Property(s => s.Customer)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.TotalAmount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.TotalAmountDiscont)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(s => s.Branch)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.IsCanceled)
                .IsRequired();

            // Relationships  
            builder.HasMany(s => s.Products)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
