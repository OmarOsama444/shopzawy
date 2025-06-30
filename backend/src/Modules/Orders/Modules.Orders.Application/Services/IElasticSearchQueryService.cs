using Modules.Orders.Domain.Elastic;
using Nest;

namespace Modules.Orders.Application.Services;

public interface IElasticSearchQueryService
{
    public Task<ISearchResponse<ProductDocument>> SearchProductsAsync(
        string? queryText = null,
        string? vendorId = null,
        string? brandId = null,
        List<string>? categoryIds = null,
        float? minPrice = null,
        float? maxPrice = null,
        List<VariationFilter<string>>? stringVariationFilters = null,
        List<VariationFilter<float>>? numericVariationFilters = null,
        int page = 1,
        int pageSize = 20,
        CancellationToken cancellationToken = default);
}

public class VariationFilter<T>
{
    public Guid SpecId { get; set; }
    public T Value { get; set; } = default!;
}