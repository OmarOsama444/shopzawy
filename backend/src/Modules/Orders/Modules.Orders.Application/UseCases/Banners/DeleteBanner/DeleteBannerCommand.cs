using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;

namespace Modules.Orders.Application.UseCases.Banners.DeleteBanner;

public record DeleteBannerCommand(Guid id) : ICommand<Guid>;

public sealed class DeleteBannerCommandHandler(
    IBannerRepository bannerRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<DeleteBannerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await bannerRepository.GetByIdAsync(request.id);
        if (banner is null)
            return new NotFoundException("Banner.NotFound", $"Banner with id {request.id} not found");
        bannerRepository.Remove(banner);
        await unitOfWork.SaveChangesAsync();
        return request.id;
    }
}

internal class DeleteBannerCommandValidator : AbstractValidator<DeleteBannerCommand>
{
    public DeleteBannerCommandValidator()
    {
        RuleFor(x => x.id).NotEmpty();
    }
}

