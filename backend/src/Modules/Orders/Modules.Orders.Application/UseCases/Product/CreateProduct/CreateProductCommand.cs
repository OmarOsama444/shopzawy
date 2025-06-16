using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateProduct;

public record CreateProductCommand(
    IDictionary<Language, string> productNames,
    IDictionary<Language, string> longDescriptions,
    IDictionary<Language, string> shortDescriptions,
    ICollection<string> tags,
    WeightUnit weightUnit,
    DimensionUnit dimensionUnit,
    Guid vendorId,
    Guid brandId,
    Guid categoryId,
    ICollection<ProductItemDto> productItems) : ICommand<Guid>;

public sealed class CreateProductCommandHandler(
    IProductService productService) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var result = await productService.CreateProductWithItem(
            request.productNames,
            request.longDescriptions,
            request.shortDescriptions,
            request.weightUnit,
            request.dimensionUnit,
            request.tags,
            request.vendorId,
            request.brandId,
            request.categoryId,
            request.productItems);

        return result;
    }
}

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.productNames)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.productNames.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.longDescriptions)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.longDescriptions.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.shortDescriptions)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.shortDescriptions.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.weightUnit).NotEmpty();
        RuleFor(c => c.dimensionUnit).NotEmpty();
        RuleFor(c => c.productItems).NotEmpty();
        RuleForEach(c => c.productItems)
            .SetValidator(new productItemValidator());

        RuleFor(x => x)
        .Must(HaveConsistentLanguageKeys)
        .WithMessage("All localized fields must have the same set of language codes (keys).");
    }
    private bool HaveConsistentLanguageKeys(CreateProductCommand cmd)
    {
        var keys1 = cmd.productNames?.Keys?.OrderBy(k => k).ToArray();
        var keys2 = cmd.longDescriptions?.Keys?.OrderBy(k => k).ToArray();
        var keys3 = cmd.shortDescriptions?.Keys?.OrderBy(k => k).ToArray();

        if (keys1 == null || keys2 == null || keys3 == null)
            return false;

        return keys1.SequenceEqual(keys2) && keys1.SequenceEqual(keys3);
    }
}

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