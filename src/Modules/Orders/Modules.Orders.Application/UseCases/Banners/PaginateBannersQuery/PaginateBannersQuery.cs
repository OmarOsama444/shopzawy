using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Banners.PaginateBannersQuery;

public record PaginateBannersQuery(int pageNumber, int pageSize, string? Title, bool? isActive) : IQuery<PaginationResponse<BannerResponse>>;

public sealed class PaginateBannersQueryHandler(IBannerRepository bannerRepository) : IQueryHandler<PaginateBannersQuery, PaginationResponse<BannerResponse>>
{
    public async Task<Result<PaginationResponse<BannerResponse>>> Handle(PaginateBannersQuery request, CancellationToken cancellationToken)
    {
        var banners = await bannerRepository.Paginate(request.pageNumber, request.pageSize, request.Title, request.isActive);
        int total = await bannerRepository.Total(request.Title, request.isActive);
        return new PaginationResponse<BannerResponse>(
            banners,
            total,
            request.pageSize,
            request.pageNumber
        );
    }
}

internal class PaginateBannersQueryValidator : AbstractValidator<PaginateBannersQuery>
{
    public PaginateBannersQueryValidator()
    {
        RuleFor(x => x.pageNumber).GreaterThan(0);
        RuleFor(x => x.pageSize).LessThan(51).GreaterThan(0);
    }
}