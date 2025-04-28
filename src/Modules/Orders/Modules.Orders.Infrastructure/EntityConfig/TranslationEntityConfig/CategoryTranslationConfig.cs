using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Infrastructure.EntityConfig.TranslationEntityConfig;

public class CategoryTranslationConfig : IEntityTypeConfiguration<CategoryTranslation>
{
    public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Name)
            .IsUnique();

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
    }

}
