using Common.Application.Messaging;
using Common.Domain;
using FluentValidation;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.Elastic;

namespace Modules.Orders.Application.UseCases.Products.PaginateProducts;

public record PaginateProductQuery(Guid? Query, Guid? VendorId, Guid? BrandId, List<Guid>? CategoryIds, float? MinPrice, float? MaxPrice, List<VariationFilter<string>>? StringOptions, List<VariationFilter<float>>? NumericOptions, int PageNumber, int PageSize) : IQuery<PaginationResponse<ProductDocument>>;
public sealed class PaginateProductQueryHandler(IElasticSearchQueryService elasticSearchQueryService) : IQueryHandler<PaginateProductQuery, PaginationResponse<ProductDocument>>
{
    public async Task<Result<PaginationResponse<ProductDocument>>> Handle(PaginateProductQuery request, CancellationToken cancellationToken)
    {
        var result = await elasticSearchQueryService.SearchProductsAsync(
            request.Query?.ToString(),
            request.VendorId?.ToString(),
            request.BrandId?.ToString(),
            request.CategoryIds?.Select(x => x.ToString()).ToList(),
            request.MinPrice,
            request.MaxPrice,
            request.StringOptions,
            request.NumericOptions,
            request.PageNumber,
            request.PageSize,
            cancellationToken
            );
        var data = result.Documents.ToList();
        var total = (int)result.Total;
        PaginationResponse<ProductDocument> paginationResponse = new(data, total, request.PageSize, request.PageNumber);
        return paginationResponse;
    }
}
internal class PaginateProductQueryValidator : AbstractValidator<PaginateProductQuery>
{
    public PaginateProductQueryValidator() { }
}