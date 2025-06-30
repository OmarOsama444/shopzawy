using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

public class ProductItemOptionColorConfig : IEntityTypeConfiguration<ProductItemOptionColor>
{
    public void Configure(EntityTypeBuilder<ProductItemOptionColor> builder)
    {
        builder.HasKey(x => new { x.ProductItemId, x.SpecificationId, x.ColorCode });
        builder.HasOne(x => x.ProductItem)
            .WithMany(x => x.ProductItemOptionColors)
            .HasForeignKey(x => x.ProductItemId);

        builder.HasOne(x => x.Specification)
       .WithMany(x => x.ProductItemOptionColors)
       .HasForeignKey(x => x.SpecificationId);

        builder.HasOne(x => x.Color)
        .WithMany(x => x.ProductItemOptionColors)
        .HasForeignKey(x => x.ColorCode);
    }

}
