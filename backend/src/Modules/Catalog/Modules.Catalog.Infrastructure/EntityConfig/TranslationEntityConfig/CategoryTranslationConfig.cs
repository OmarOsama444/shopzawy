using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Infrastructure.EntityConfig.TranslationEntityConfig;

public class CategoryTranslationConfig : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {
        builder.HasKey(x => new { x.CategoryId, x.LangCode });

        builder.Property(x => x.Name)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.CategoryTranslations)
            .HasForeignKey(x => x.CategoryId);

        builder
            .HasIndex(x => new { x.CategoryId, x.LangCode })
            .IsUnique();

        builder
            .HasData(
                CategoryTranslation.Seed()
            );
    }

}
