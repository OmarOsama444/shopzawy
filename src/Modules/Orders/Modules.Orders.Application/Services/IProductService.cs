using Modules.Common.Domain;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface IProductService
{
        public Task<Result<Guid>> CreateProduct(
                IDictionary<Language, string> product_name,
                IDictionary<Language, string> long_description,
                IDictionary<Language, string> short_description,
                WeightUnit weightUnit,
                DimensionUnit dimensionUnit,
                ICollection<string> tags,
                Guid vendorId,
                Guid brandId,
                Guid categoryId);
        public Task<Result<ICollection<Guid>>> CreateProductItems(
         Guid productId,
         ICollection<product_item> product_Items);
        public Task<Result<Guid>> CreateProductWithItem(
                IDictionary<Language, string> product_name,
                IDictionary<Language, string> long_description,
                IDictionary<Language, string> short_description,
                WeightUnit weight_unit,
                DimensionUnit dimension_unit,
                ICollection<string> tags,
                Guid vendor_id,
                Guid brand_id,
                Guid category_id,
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
