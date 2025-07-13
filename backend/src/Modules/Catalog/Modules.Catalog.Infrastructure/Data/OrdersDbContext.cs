using Common.Application;
using Common.Infrastructure.Inbox;
using Common.Infrastructure.Outbox;
using Microsoft.EntityFrameworkCore;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Domain.Entities.Views;
using Modules.Catalog.Infrastructure.EntityConfig;
using Modules.Catalog.Infrastructure.EntityConfig.TranslationEntityConfig;
using Modules.Catalog.Infrastructure.EntityConfig.ViewsEntityConfig;

namespace Modules.Catalog.Infrastructure.Data;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> Options) :
    DbContext(Options), IOrdersDbContext
{
    #region Entities
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Banner> banners { get; set; }
    public DbSet<ProductItem> ProductItems { get; set; }
    public DbSet<ProductItemOptions> ProductItemOptions { get; set; }
    public DbSet<ProductItemOptionColor> ProductItemOptionColors { get; set; }
    public DbSet<ProductItemOptionNumeric> ProductItemOptionNumerics { get; set; }
    public DbSet<Specification> Specifications { get; set; }
    public DbSet<SpecificationOption> SpecificationOptions { get; set; }
    public DbSet<CategorySpec> CategorySpecs { get; set; }
    #endregion
    #region Translations
    public DbSet<ProductTranslation> ProductTranslations { get; set; }
    public DbSet<BrandTranslation> BrandTranslations { get; set; }
    public DbSet<CategoryTranslation> CategoryTranslations { get; set; }
    public DbSet<SpecificationTranslation> SpecificationTranslations { get; set; }
    #endregion
    #region Views
    public DbSet<CategoryStatistics> CategoryStatistics { get; set; }
    public DbSet<SpecificationStatistics> SpecificationStatistics { get; set; }
    #endregion
    #region OutBox - Inbox Messages
    public DbSet<OutboxMessage> outboxMessages { get; set; }
    public DbSet<OutboxConsumerMessage> outboxConsumerMessages { get; set; }
    public DbSet<InboxMessage> inboxMessages { get; set; }
    public DbSet<InboxConsumerMessage> inboxConsumerMessages { get; set; }
    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Catalog);
        modelBuilder.ApplyConfiguration<ProductItemOptions>(new ProductItemOptionsConfig());
        modelBuilder.ApplyConfiguration<ProductItem>(new ProductItemConfig());
        modelBuilder.ApplyConfiguration<SpecificationOption>(new SpecOptionConfig());
        modelBuilder.ApplyConfiguration<Banner>(new BannerConfig());
        modelBuilder.ApplyConfiguration<Brand>(new BrandConfig());
        modelBuilder.ApplyConfiguration<Category>(new CategoryConfig());
        modelBuilder.ApplyConfiguration<Specification>(new SpecConfig());
        modelBuilder.ApplyConfiguration<Product>(new ProductConfig());
        modelBuilder.ApplyConfiguration<Vendor>(new VendorConfig());
        modelBuilder.ApplyConfiguration<Color>(new ColorConfig());
        modelBuilder.ApplyConfiguration<CategorySpec>(new CategorySpecConfig());
        modelBuilder.ApplyConfiguration<BrandTranslation>(new BrandTranslationConfig());
        modelBuilder.ApplyConfiguration<ProductTranslation>(new ProductTranslationConfig());
        modelBuilder.ApplyConfiguration<CategoryTranslation>(new CategoryTranslationConfig());
        modelBuilder.ApplyConfiguration<SpecificationTranslation>(new SpecificationTranslationConfig());
        modelBuilder.ApplyConfiguration<CategoryStatistics>(new CategoryStatisticsConfig());
        modelBuilder.ApplyConfiguration<SpecificationStatistics>(new SpecificationStatisticsConfig());
        modelBuilder.ApplyConfiguration<ProductItemOptionNumeric>(new ProductItemOptionNumericConfig());
        modelBuilder.ApplyConfiguration<ProductItemOptionColor>(new ProductItemOptionColorConfig());
        // outbox-inbox messages config
        modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<OutboxConsumerMessage>(new OutboxConsumerMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxMessage>(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration<InboxConsumerMessage>(new InboxConsumerMessageConfiguration());
    }
}
