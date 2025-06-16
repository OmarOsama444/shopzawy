using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig.TranslationEntityConfig;

public class ProductTranslationConfig : IEntityTypeConfiguration<ProductTranslation>
{
    public void Configure(EntityTypeBuilder<ProductTranslation> builder)
    {
        builder
            .HasKey(pt => pt.Id);

        builder
            .HasIndex(pt => new { pt.ProductId, pt.LangCode })
            .IsUnique();

    }

}
