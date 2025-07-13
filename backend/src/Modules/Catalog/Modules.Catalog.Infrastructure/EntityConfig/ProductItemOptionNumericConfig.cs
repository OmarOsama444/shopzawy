using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class ProductItemOptionNumericConfig : IEntityTypeConfiguration<ProductItemOptionNumeric>
{
    public void Configure(EntityTypeBuilder<ProductItemOptionNumeric> builder)
    {
        builder.HasKey(x => new { x.ProductItemId, x.SpecificationId, x.Value });
        builder.HasOne(x => x.ProductItem)
            .WithMany(x => x.ProductItemOptionNumerics)
            .HasForeignKey(x => x.ProductItemId);

        builder.HasOne(x => x.Specification)
       .WithMany(x => x.ProductItemOptionNumerics)
       .HasForeignKey(x => x.SpecificationId);
    }

}
