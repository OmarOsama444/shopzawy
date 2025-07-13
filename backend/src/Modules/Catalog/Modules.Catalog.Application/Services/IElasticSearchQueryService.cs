using Modules.Catalog.Domain.Elastic;
using Nest;

namespace Modules.Catalog.Application.Services;

public interface IElasticSearchQueryService
{
    public Task<ISearchResponse<ProductDocument>> SearchProductsAsync(
        string? queryText = null,
        string? vendorId = null,
        string? brandId = null,
        string? categoryIds = null,
        float? minPrice = null,
        float? maxPrice = null,
        List<VariationFilter<string>>? colorVariationFilters = null,
        List<VariationFilter<string>>? stringVariationFilters = null,
        List<NumericVariationFilter>? numericVariationFilters = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
}

public class NumericVariationFilter
{
    public Guid SpecId { get; set; }
    public float? Lte { get; set; } = null;
    public float? Gte { get; set; } = null;
}

public class VariationFilter<T>
{
    public Guid SpecId { get; set; }
    public T Value { get; set; } = default!;
}