using Common.Application.Validators;
using FluentValidation;
using Modules.Catalog.Application.Services.Dtos;

namespace Modules.Catalog.Application.UseCases.Products.CreateProduct;

internal class productItemValidator : AbstractValidator<ProductItemDto>
{
    public productItemValidator()
    {
        RuleFor(x => x.StockKeepingUnit).NotEmpty();
        RuleFor(x => x.QuantityInStock);
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Urls).NotEmpty();
        RuleForEach(x => x.Urls).Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(c => c.Weight).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Width).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Height).NotEmpty().GreaterThan(0);
        RuleFor(c => c.Length).NotEmpty().GreaterThan(0);
        RuleForEach(x => x.Urls)
            .NotEmpty()
            .WithMessage("Urls Can't be empty")
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}