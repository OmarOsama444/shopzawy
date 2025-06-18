using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Infrastructure.EntityConfig.ViewsEntityConfig;

public class CategoryStatisticsConfig : IEntityTypeConfiguration<CategoryStatistics>
{
    public void Configure(EntityTypeBuilder<CategoryStatistics> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.CreatedOn);
    }

}
