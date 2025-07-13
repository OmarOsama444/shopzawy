using Common.Application.Validators;
using FluentValidation;

namespace Modules.Catalog.Application.UseCases.ProductItems.UpdateProductItem;

internal class UpdateProductItemCommandValidator : AbstractValidator<UpdateProductItemCommand>
{
    public UpdateProductItemCommandValidator()
    {
        RuleFor(x => x.ProductItemId)
            .NotEmpty();
        RuleFor(x => x.QuantityInStock)
            .GreaterThan(0)
            .When(x => x != null);
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .When(x => x.Price.HasValue);
        RuleFor(x => x.Width)
            .GreaterThan(0)
            .When(x => x.Width.HasValue);
        RuleFor(x => x.Height)
            .GreaterThan(0)
            .When(x => x.Height.HasValue);
        RuleFor(x => x.Length)
            .GreaterThan(0)
            .When(x => x.Length.HasValue);
        RuleFor(x => x.Weight)
            .GreaterThan(0)
            .When(x => x.Weight.HasValue);
        RuleForEach(x => x.AddUrls)
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
        RuleForEach(x => x.RemoveUrls)
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}