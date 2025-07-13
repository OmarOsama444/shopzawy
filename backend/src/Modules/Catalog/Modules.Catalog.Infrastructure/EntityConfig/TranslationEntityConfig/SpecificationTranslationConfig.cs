
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Infrastructure.EntityConfig.TranslationEntityConfig;

public class SpecificationTranslationConfig : IEntityTypeConfiguration<SpecificationTranslation>
{
    public void Configure(EntityTypeBuilder<SpecificationTranslation> builder)
    {
        builder
            .HasKey(x => x.Id);

        builder
            .HasOne(x => x.specification)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.SpecId);

        builder
            .Property(x => x.Name)
            .HasMaxLength(100);

        builder
            .HasIndex(x => x.Name);

        builder
            .HasIndex(x => new { x.SpecId, x.LangCode })
            .IsUnique();
    }

}
