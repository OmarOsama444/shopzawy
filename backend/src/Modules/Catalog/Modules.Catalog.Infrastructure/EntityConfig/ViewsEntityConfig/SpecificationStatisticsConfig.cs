using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Catalog.Domain.Entities.Views;

namespace Modules.Catalog.Infrastructure.EntityConfig.ViewsEntityConfig;

public class SpecificationStatisticsConfig : IEntityTypeConfiguration<SpecificationStatistics>
{
    public void Configure(EntityTypeBuilder<SpecificationStatistics> builder)
    {
        builder.HasKey(x => new { x.Id, x.Value });
        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.Value);
        builder.HasIndex(x => x.CreatedOnUtc);
        builder.HasIndex(x => x.TotalProducts);
    }

}