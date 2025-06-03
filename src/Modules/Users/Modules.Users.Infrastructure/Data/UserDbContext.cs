using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Infrastructure;
using Modules.Common.Infrastructure.Inbox;
using Modules.Common.Infrastructure.Outbox;
using Modules.Users.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Infrastructure.EntityConfig;

namespace Modules.Users.Infrastructure;

public class UserDbContext(DbContextOptions<UserDbContext> Options) :
    IdentityDbContext<User, Role, Guid, IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, RoleClaim, UserToken>(Options)
{
    public DbSet<User> users { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<RoleClaim> roleClaims { get; set; }
    public DbSet<UserRole> userRoles { get; set; }
    public DbSet<UserToken> userTokens { get; set; }
    public DbSet<OutboxMessage> outboxMessages { get; set; }
    public DbSet<OutboxConsumerMessage> outboxConsumerMessages { get; set; }
    public DbSet<InboxMessage> inboxMessages { get; set; }
    public DbSet<InboxConsumerMessage> inboxConsumerMessages { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);
        modelBuilder.ApplyConfiguration<User>(new UserConfig());
        modelBuilder.ApplyConfiguration<Role>(new RoleConfig());
        modelBuilder.ApplyConfiguration<RoleClaim>(new RoleClaimConfig());
        modelBuilder.ApplyConfiguration<UserRole>(new UserRoleConfig());
        modelBuilder.ApplyConfiguration<UserToken>(new UserTokenConfig());
        modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<OutboxConsumerMessage>(new OutboxConsumerMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxMessage>(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxConsumerMessage>(new InboxConsumerMessageConfiguration());
    }
}
