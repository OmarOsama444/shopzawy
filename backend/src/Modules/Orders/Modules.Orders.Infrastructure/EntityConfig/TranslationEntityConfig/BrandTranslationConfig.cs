
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig.TranslationEntityConfig;

public class BrandTranslationConfig : IEntityTypeConfiguration<BrandTranslation>
{
    public void Configure(EntityTypeBuilder<BrandTranslation> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasIndex(x => new { x.BrandId, x.LangCode })
            .IsUnique();
    }

}
