using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

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
        builder.Property(p => p.ImageUrls)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .ToList()
            );

        builder
            .HasIndex(p => p.CreatedOn);
    }

}
