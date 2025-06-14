using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig;

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
