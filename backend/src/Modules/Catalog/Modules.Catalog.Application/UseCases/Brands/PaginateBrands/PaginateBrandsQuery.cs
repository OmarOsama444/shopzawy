using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using FluentValidation;
using Modules.Catalog.Application.Repositories;

namespace Modules.Catalog.Application.UseCases.Brands.PaginateBrands;

public record PaginateBrandsQuery(int pageNumber, int pageSize, string? nameField, Language langCode) : IQuery<PaginationResponse<TranslatedBrandResponseDto>>;

public sealed class PaginateBrandsQueryHandler(IBrandRepository brandRepository) : IQueryHandler<PaginateBrandsQuery, PaginationResponse<TranslatedBrandResponseDto>>
{
    public async Task<Result<PaginationResponse<TranslatedBrandResponseDto>>> Handle(PaginateBrandsQuery request, CancellationToken cancellationToken)
    {
        ICollection<TranslatedBrandResponseDto> brandResponses = await brandRepository.Paginate(request.pageNumber, request.pageSize, request.nameField, request.langCode);
        int totalBrands = await brandRepository.TotalBrands(request.nameField, request.langCode);
        return new PaginationResponse<TranslatedBrandResponseDto>(brandResponses, totalBrands, request.pageSize, request.pageNumber);
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
