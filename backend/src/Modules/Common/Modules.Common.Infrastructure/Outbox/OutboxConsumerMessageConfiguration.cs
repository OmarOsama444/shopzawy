using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Modules.Common.Infrastructure.Outbox;

public class OutboxConsumerMessageConfiguration : IEntityTypeConfiguration<OutboxConsumerMessage>
{
    public void Configure(EntityTypeBuilder<OutboxConsumerMessage> builder)
    {
        builder.HasKey(msg => new { msg.id, msg.HandlerName });
    }

}
