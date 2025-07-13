using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

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
            .HasMany(p => p.ProductTranslations)
            .WithOne(pt => pt.Product)
            .HasForeignKey(pt => pt.ProductId);

        builder
            .HasIndex(p => p.CreatedOn);

        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

    }
}
