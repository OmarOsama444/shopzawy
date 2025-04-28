using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .HasMany(p => p.ProductItems)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId);

        builder
            .HasIndex(p => p.CreatedOn);

        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        // builder
        //     .HasIndex(b => new { b.ProductName, b.ShortDescription, b.LongDescription })
        //     .HasMethod("GIN")
        //     .IsTsVectorExpressionIndex("english");

        builder.Property(p => p.Tags)
        .HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()
        )
        .HasColumnName("Tags")
        .HasColumnType("TEXT");

    }
}
