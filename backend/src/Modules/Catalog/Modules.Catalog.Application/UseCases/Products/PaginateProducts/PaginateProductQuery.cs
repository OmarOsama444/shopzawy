using Common.Application.Messaging;
using Common.Domain;
using FluentValidation;
using Modules.Catalog.Application.Services;
using Modules.Catalog.Domain.Elastic;

namespace Modules.Catalog.Application.UseCases.Products.PaginateProducts;

public record PaginateProductQuery(Guid? Query, Guid? VendorId, Guid? BrandId, Guid? CategoryId, float? MinPrice, float? MaxPrice, List<VariationFilter<string>>? StringOptions, List<NumericVariationFilter>? NumericOptions, List<VariationFilter<string>> ColorOptions, int PageNumber, int PageSize) : IQuery<PaginationResponse<ProductDocument>>;
public sealed class PaginateProductQueryHandler(IElasticSearchQueryService elasticSearchQueryService) : IQueryHandler<PaginateProductQuery, PaginationResponse<ProductDocument>>
{
    public async Task<Result<PaginationResponse<ProductDocument>>> Handle(PaginateProductQuery request, CancellationToken cancellationToken)
    {
        var result = await elasticSearchQueryService.SearchProductsAsync(
            request.Query?.ToString(),
            request.VendorId?.ToString(),
            request.BrandId?.ToString(),
            request.CategoryId?.ToString(),
            request.MinPrice,
            request.MaxPrice,
            request.ColorOptions,
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