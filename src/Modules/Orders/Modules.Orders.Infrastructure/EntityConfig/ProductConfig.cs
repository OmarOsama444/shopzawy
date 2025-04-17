using System.Text.Json;
using Microsoft.EntityFrameworkCore;
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
            .HasMany(p => p.ProductCategories)
            .WithOne(pc => pc.Product)
            .HasForeignKey(pc => pc.ProductId);

        builder
            .HasMany(p => p.ProductDiscounts)
            .WithOne(pd => pd.Product)
            .HasForeignKey(pd => pd.ProductId);

        builder
            .HasMany(p => p.ProductItems)
            .WithOne(p => p.Product)
            .HasForeignKey(p => p.ProductId);

        builder
            .HasIndex(p => p.CreatedOn);

        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryName);

        builder.Property(p => p.Tags)
        .HasConversion(
            v => JsonSerializer.Serialize(v, JsonDefaults.Options),
            v => JsonSerializer.Deserialize<ICollection<string>>(v, JsonDefaults.Options) ?? new List<string>()
        )
        .HasColumnName("Tags")
        .HasColumnType("TEXT");

    }
}
