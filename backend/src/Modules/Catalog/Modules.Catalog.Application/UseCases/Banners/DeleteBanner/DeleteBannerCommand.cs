using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.Exceptions;
using FluentValidation;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;

namespace Modules.Catalog.Application.UseCases.Banners.DeleteBanner;

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

