using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Banners.GetActiveBanners;

public record GetActiveBannerQuery() : IQuery<ICollection<GetActiveBannerQueryResponse>>;
public sealed class GetActiveBannerQueryHandler(IBannerRepository bannerRepository) : IQueryHandler<GetActiveBannerQuery, ICollection<GetActiveBannerQueryResponse>>
{
    public async Task<Result<ICollection<GetActiveBannerQueryResponse>>> Handle(GetActiveBannerQuery request, CancellationToken cancellationToken)
    {
        var banners = await bannerRepository.GetBannerByActive(true);
        var bannersResponse = banners.Select(x => new GetActiveBannerQueryResponse(
            x.Id,
            x.Title,
            x.Description,
            x.Link,
            x.Active,
            x.Position.ToString(),
            x.Size.ToString()
        )).ToList();
        return bannersResponse;
    }
}
internal class GetActiveBannerQueryValidator : AbstractValidator<GetActiveBannerQuery>
{
    public GetActiveBannerQueryValidator() { }
}
public record GetActiveBannerQueryResponse(
    Guid Id,
    string Title,
    string Description,
    string Link,
    bool Active,
    string Position,
    string Size
);

