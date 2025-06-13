using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Banners.CreateBanner;

public record CreateBannerCommand(
            string title,
            string description,
            string link,
            BannerPosition position,
            BannerSize size,
            bool active) : ICommand<Guid>;

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

internal class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
{
    public CreateBannerCommandValidator()
    {
        RuleFor(x => x.title).NotEmpty();
        RuleFor(x => x.description).NotEmpty();
        RuleFor(x => x.link).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(x => x.position).NotEmpty();
        RuleFor(x => x.size).NotEmpty();
        RuleFor(x => x.active).NotEmpty();
    }
}
