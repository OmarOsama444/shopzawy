using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MassTransit.SagaStateMachine;
using Microsoft.EntityFrameworkCore;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;
using Modules.Orders.Infrastructure.Data;

namespace Modules.Orders.Infrastructure.Services;

public class ProductService(OrdersDbContext ordersDbContext) : IProductService
{
    public async Task<Result<Guid>> CreateProduct(
        IDictionary<Language, string> product_name,
        IDictionary<Language, string> long_description,
        IDictionary<Language, string> short_description,
        WeightUnit weightUnit,
        DimensionUnit dimensionUnit,
        ICollection<string> tags,
        Guid vendorId,
        Guid brandId,
        Guid categoryId)
    {
        Category? category = await ordersDbContext
            .Categories
            .FirstOrDefaultAsync(x => x.Id == categoryId);

        if (category is null)
            return new CategoryNotFoundException(categoryId);
        if (ordersDbContext.Categories.Any(x => x.ParentCategoryId == category.Id))
            return new ConflictException("Category.Conflict", "you can only add products to leaf categorys");
        if (!await ordersDbContext.Vendors.AnyAsync(x => x.Id == vendorId))
            return new VendorNotFoundException(vendorId);
        if (!await ordersDbContext.Brands.AnyAsync(x => x.Id == brandId))
            return new BrandNotFoundException(brandId);

        var product = Product.Create(
            weightUnit,
            dimensionUnit,
            tags,
            vendorId,
            brandId,
            categoryId
        );

        ordersDbContext.Products.Add(product);
        foreach (Language langCode in product_name.Keys)
        {
            ProductTranslation productTranslation = ProductTranslation.Create(
                productId: product.Id,
                langCode: langCode,
                productName: product_name[langCode],
                longDescription: long_description[langCode],
                shortDescription: short_description[langCode]);

            ordersDbContext.ProductTranslations.Add(productTranslation);
        }
        await ordersDbContext.SaveChangesAsync();

        return product.Id;
    }

    public async Task<Result<ICollection<Guid>>> CreateProductItems(
        Guid productId,
        ICollection<product_item> product_items)
    {
        ICollection<Guid> ProductItemIds = new HashSet<Guid>();
        Product? product = await ordersDbContext
            .Products
            .Include(p => p.Category)
            .ThenInclude(p => p.CategorySpecs)
            .ThenInclude(p => p.Specification)
            .FirstOrDefaultAsync(x => x.Id == productId);
        if (product == null)
            return new ProductNotFoundException(productId);

        ICollection<Specification> specifications = product.Category.CategorySpecs.Select(x => x.Specification).ToList();

        foreach (var product_item in product_items)
        {
            ProductItem productItem = ProductItem.Create(
                product_item.stock_keeping_unit,
                product_item.quantity_in_stock,
                product_item.price,
                product_item.width,
                product_item.length,
                product_item.height,
                product_item.weight,
                productId,
                product_item.urls);
            ordersDbContext.ProductItems.Add(productItem);
            foreach (Specification specification in specifications)
            {
                if (product_item.spec_options.ContainsKey(specification.Id)
                    && ordersDbContext
                        .SpecificationOptions
                        .Any(
                            x => x.SpecificationId == specification.Id
                            &&
                            x.Id == product_item.spec_options[specification.Id]
                            )
                        )
                {
                    ProductItemOptions productItemOptions = ProductItemOptions.Create(
                        productItem.Id,
                        product_item.spec_options[specification.Id]
                    );

                    ordersDbContext.ProductItemOptions.Add(productItemOptions);
                }
                else
                {
                    return new SpecificationNotFoundException(specification.Id);
                }
            }
            await ordersDbContext.SaveChangesAsync();
            ProductItemIds.Add(productItem.Id);
        }

        return Result<ICollection<Guid>>.Success(ProductItemIds);
    }

    public async Task<Result<Guid>> CreateProductWithItem(
        IDictionary<Language, string> product_name,
        IDictionary<Language, string> long_description,
        IDictionary<Language, string> short_description,
        WeightUnit weight_unit,
        DimensionUnit dimension_unit,
        ICollection<string> tags,
        Guid vendor_id,
        Guid brand_id,
        Guid category_id,
        ICollection<product_item> product_items)
    {
        await using var transaction = await ordersDbContext.Database.BeginTransactionAsync();

        try
        {
            var productResult = await CreateProduct(
                product_name,
                long_description,
                short_description,
                weight_unit,
                dimension_unit,
                tags,
                vendor_id,
                brand_id,
                category_id
            );

            if (!productResult.isSuccess)
            {
                await transaction.RollbackAsync();
                return productResult.exception!;
            }
            var productItemResult = await CreateProductItems(
                    productResult.Value,
                    product_items
                );
            if (!productItemResult.isSuccess)
            {
                await transaction.RollbackAsync();
                return productItemResult.exception!;
            }
            await transaction.CommitAsync();
            return productResult.Value;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return new Exception("An error occurred while creating product and product item.", ex);
        }
    }

}

