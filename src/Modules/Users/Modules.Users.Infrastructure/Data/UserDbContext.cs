using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure.Inbox;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure.EntityConfig;

namespace Modules.Users.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> Options) :
    DbContext(Options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<UnverifiedUser> unverifiedUsers { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<EmailVerificationToken> emailVerificationTokens { get; set; }
    public DbSet<OutboxMessage> outboxMessages { get; set; }
    public DbSet<OutboxConsumerMessage> outboxConsumerMessages { get; set; }
    public DbSet<InboxMessage> inboxMessages { get; set; }
    public DbSet<InboxConsumerMessage> inboxConsumerMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("USERS");
        modelBuilder.ApplyConfiguration<User>(new UserConfig());
        modelBuilder.ApplyConfiguration<RefreshToken>(new RefreshTokenConfig());
        modelBuilder.ApplyConfiguration<EmailVerificationToken>(new EmailVerificationTokenConfig());
        modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<UnverifiedUser>(new UnverifiedUserConfig());
        modelBuilder.ApplyConfiguration<OutboxConsumerMessage>(new OutboxConsumerMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxMessage>(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxConsumerMessage>(new InboxConsumerMessageConfiguration());
    }
}
