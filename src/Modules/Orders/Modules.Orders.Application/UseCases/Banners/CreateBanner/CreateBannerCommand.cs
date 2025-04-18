using System.Data;
using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.Banners.CreateBanner;

public record CreateBannerCommand(
            string Title,
            string Description,
            string Link,
            BannerPosition Position,
            BannerSize Size,
            bool Active) : ICommand<Guid>;

public sealed class CreateBannerCommandHandler(
    IBannerRepository bannerRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateBannerCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = Banner.Create(request.Title, request.Description, request.Link, request.Position, request.Size, request.Active);
        bannerRepository.Add(banner);
        await unitOfWork.SaveChangesAsync();
        return banner.Id;
    }
}

internal class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommand>
{
    public CreateBannerCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Link).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(x => x.Position).NotEmpty();
        RuleFor(x => x.Size).NotEmpty();
        RuleFor(x => x.Active).NotEmpty();
    }
}
