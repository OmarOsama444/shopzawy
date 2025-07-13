using Common.Application.Messaging;
using Common.Domain;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.UseCases.Banners.CreateBanner;

public sealed class CreateBannerCommandHandler(
    IBannerRepository bannerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateBannerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = Banner.Create(request.title, request.description, request.link, request.position, request.size, request.active);
        bannerRepository.Add(banner);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return banner.Id;
    }
}
