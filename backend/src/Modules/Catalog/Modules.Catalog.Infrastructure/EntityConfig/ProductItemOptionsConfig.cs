using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Infrastructure.EntityConfig;

public class ProductItemOptionsConfig : IEntityTypeConfiguration<ProductItemOptions>
{
    public void Configure(EntityTypeBuilder<ProductItemOptions> builder)
    {
        builder.HasKey(x => new
        {
            x.ProductItemId,
            x.SpecificationId,
            x.Value
        });
    }

}
