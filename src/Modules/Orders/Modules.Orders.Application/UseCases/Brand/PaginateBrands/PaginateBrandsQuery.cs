using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.PaginateBrands;

public record PaginateBrandsQuery(int pageNumber, int pageSize, string? nameField, Language langCode) : IQuery<PaginationResponse<BrandResponse>>;

public sealed class PaginateBrandsQueryHandler(IBrandRepository brandRepository) : IQueryHandler<PaginateBrandsQuery, PaginationResponse<BrandResponse>>
{
    public async Task<Result<PaginationResponse<BrandResponse>>> Handle(PaginateBrandsQuery request, CancellationToken cancellationToken)
    {
        ICollection<BrandResponse> brandResponses = await brandRepository.Paginate(request.pageNumber, request.pageSize, request.nameField, request.langCode);
        int totalBrands = await brandRepository.TotalBrands(request.nameField);
        return new PaginationResponse<BrandResponse>(brandResponses, totalBrands, request.pageSize, request.pageNumber);
    }
}

internal class PaginateBrandsQueryValidator : AbstractValidator<PaginateBrandsQuery>
{
    public PaginateBrandsQueryValidator()
    {
        RuleFor(p => p.pageNumber).NotEmpty().GreaterThan(0);
        RuleFor(p => p.pageSize).NotEmpty().GreaterThan(0).LessThan(50);
    }
}
