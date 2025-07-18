using Common.Application.Validators;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.Banners.CreateBanner;

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
