using Common.Application.Messaging;
using Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.UseCases.Banners.CreateBanner;

public sealed class CreateBannerCommandHandler(
    IBannerRepository bannerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateBannerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = Banner.Create(request.title, request.description, request.link, request.position, request.size, request.active);
        bannerRepository.Add(banner);
        await unitOfWork.SaveChangesAsync();
        return banner.Id;
    }
}
