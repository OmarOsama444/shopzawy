using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure.Inbox;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure.EntityConfig;

namespace Modules.Users.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> Options) :
    IdentityDbContext<User, Role, Guid>(Options)
{
    public DbSet<UserToken> userTokens { get; set; }
    public DbSet<OutboxMessage> outboxMessages { get; set; }
    public DbSet<OutboxConsumerMessage> outboxConsumerMessages { get; set; }
    public DbSet<InboxMessage> inboxMessages { get; set; }
    public DbSet<InboxConsumerMessage> inboxConsumerMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("USERS");
        modelBuilder.ApplyConfiguration<User>(new UserConfig());
        modelBuilder.ApplyConfiguration<Role>(new RoleConfig());
        modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<OutboxConsumerMessage>(new OutboxConsumerMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxMessage>(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxConsumerMessage>(new InboxConsumerMessageConfiguration());
    }
}
