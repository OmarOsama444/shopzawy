using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Infrastructure.EntityConfig.ViewsEntityConfig;

public class SpecificationStatisticsConfig : IEntityTypeConfiguration<SpecificationStatistics>
{
    public void Configure(EntityTypeBuilder<SpecificationStatistics> builder)
    {
        builder.HasKey(x => new { x.Id, x.Value });
        builder.HasIndex(x => x.CreatedOn);
    }

}