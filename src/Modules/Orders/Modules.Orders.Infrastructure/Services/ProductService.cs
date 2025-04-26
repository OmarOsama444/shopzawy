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
        string productName,
        string longDescription,
        string shortDescription,
        WeightUnit weightUnit,
        float weight,
        DimensionUnit dimensionUnit,
        float width,
        float length,
        float height,
        ICollection<string> tags,
        Guid vendorId,
        string brandName,
        string categoryName)
    {
        Category? category = await ordersDbContext
            .Categories
            .FirstOrDefaultAsync(x => x.CategoryName == categoryName);

        if (category is null)
            return new CategoryNotFoundException(categoryName);
        if (ordersDbContext.Categories.Any(x => x.ParentCategoryName == category.CategoryName))
            return new ConflictException("Category.Conflict", "you can only add products to leaf categorys");
        if (!await ordersDbContext.Vendors.AnyAsync(x => x.Id == vendorId))
            return new VendorNotFoundException(vendorId);
        if (!await ordersDbContext.Brands.AnyAsync(x => x.BrandName == brandName))
            return new BrandNotFoundException(brandName);

        var product = Product.Create(
            productName,
            longDescription,
            shortDescription,
            weightUnit,
            weight,
            dimensionUnit,
            width,
            length,
            height,
            tags,
            vendorId,
            brandName,
            categoryName
        );

        ordersDbContext.Products.Add(product);
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
        string product_name,
        string long_description,
        string short_description,
        WeightUnit weight_unit,
        float weight,
        DimensionUnit dimension_unit,
        float width,
        float length,
        float height,
        ICollection<string> tags,
        Guid vendor_id,
        string brand_name,
        string category_name,
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
                weight,
                dimension_unit,
                width,
                length,
                height,
                tags,
                vendor_id,
                brand_name,
                category_name
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

