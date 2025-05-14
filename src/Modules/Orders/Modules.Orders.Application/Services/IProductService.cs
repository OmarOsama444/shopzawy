using Modules.Common.Domain;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface IProductService
{
        public Task<Result<Guid>> CreateProduct(
           string productName,
           string longDescription,
           string shortDescription,
           WeightUnit weightUnit,
           DimensionUnit dimensionUnit,
           ICollection<string> tags,
           Guid vendorId,
           string brandName,
           Guid categoryId);
        public Task<Result<ICollection<Guid>>> CreateProductItems(
         Guid productId,
         ICollection<product_item> product_Items);
        public Task<Result<Guid>> CreateProductWithItem(
                string product_name,
                string long_description,
                string short_description,
                WeightUnit weight_unit,
                DimensionUnit dimension_unit,
                ICollection<string> tags,
                Guid vendor_id,
                string brand_name,
                Guid categoryId,
                ICollection<product_item> product_items);



}

public record product_item(
        string stock_keeping_unit,
        int quantity_in_stock,
        float price,
        float weight,
        float width,
        float length,
        float height,
        ICollection<string> urls,
        IDictionary<Guid, Guid> spec_options);
