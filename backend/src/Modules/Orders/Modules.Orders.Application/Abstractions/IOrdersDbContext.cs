using Microsoft.EntityFrameworkCore;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Entities.Views;

namespace Modules.Orders.Application.Abstractions;

public interface IOrdersDbContext
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
}
