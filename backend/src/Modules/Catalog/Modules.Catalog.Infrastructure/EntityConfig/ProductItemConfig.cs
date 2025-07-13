using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class ProductItemConfig : IEntityTypeConfiguration<ProductItem>
{
    public void Configure(EntityTypeBuilder<ProductItem> builder)
    {
        builder
            .HasKey(p => p.Id);
        builder
            .HasMany(p => p.ProductItemOptions)
            .WithOne(po => po.ProductItem)
            .HasForeignKey(po => po.ProductItemId);
        builder
            .HasIndex(p => p.StockKeepingUnit);
        builder
            .HasIndex(p => new { p.ProductId, p.StockKeepingUnit })
            .IsUnique();
        builder
            .HasIndex(p => p.CreatedOn);
    }

}
