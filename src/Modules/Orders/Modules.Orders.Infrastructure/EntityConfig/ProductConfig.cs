using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;
using Npgsql.Internal;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
            .HasKey(p => p.Id);

        builder
            .HasIndex(p => p.Price);

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
    }
}
