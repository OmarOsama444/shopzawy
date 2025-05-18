using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateProduct;

public record LocalizedText(IDictionary<Language, string> translations);
public record CreateProductCommand(
    LocalizedText product_names,
    LocalizedText long_descriptions,
    LocalizedText short_descriptions,
    ICollection<string> tags,
    WeightUnit weight_unit,
    DimensionUnit dimension_unit,
    Guid vendor_id,
    Guid brand_id,
    Guid category_id,
    ICollection<product_item> product_items) : ICommand<Guid>;

public sealed class CreateProductCommandHandler(
    IProductService productService) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var result = await productService.CreateProductWithItem(
            request.product_names.translations,
            request.long_descriptions.translations,
            request.short_descriptions.translations,
            request.weight_unit,
            request.dimension_unit,
            request.tags,
            request.vendor_id,
            request.brand_id,
            request.category_id,
            request.product_items);

        return result;
    }
}

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.product_names.translations)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.product_names.translations.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.long_descriptions.translations)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.long_descriptions.translations.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.short_descriptions.translations)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.short_descriptions.translations.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.weight_unit).NotEmpty();
        RuleFor(c => c.dimension_unit).NotEmpty();
        RuleFor(c => c.product_items).NotEmpty();
        RuleForEach(c => c.product_items)
            .SetValidator(new productItemValidator());

        RuleFor(x => x)
        .Must(HaveConsistentLanguageKeys)
        .WithMessage("All localized fields must have the same set of language codes (keys).");
    }
    private bool HaveConsistentLanguageKeys(CreateProductCommand cmd)
    {
        var keys1 = cmd.product_names?.translations?.Keys?.OrderBy(k => k).ToArray();
        var keys2 = cmd.long_descriptions?.translations?.Keys?.OrderBy(k => k).ToArray();
        var keys3 = cmd.short_descriptions?.translations?.Keys?.OrderBy(k => k).ToArray();

        if (keys1 == null || keys2 == null || keys3 == null)
            return false;

        return keys1.SequenceEqual(keys2) && keys1.SequenceEqual(keys3);
    }
}

internal class productItemValidator : AbstractValidator<product_item>
{
    public productItemValidator()
    {
        RuleFor(x => x.stock_keeping_unit).NotEmpty();
        RuleFor(x => x.quantity_in_stock).GreaterThan(0);
        RuleFor(x => x.price).GreaterThan(0);
        RuleFor(x => x.urls).NotEmpty();
        RuleFor(c => c.weight).NotEmpty();
        RuleFor(c => c.width).NotEmpty();
        RuleFor(c => c.height).NotEmpty();
        RuleFor(c => c.length).NotEmpty();
        RuleForEach(x => x.urls)
            .NotEmpty()
            .WithMessage("Urls Can't be empty")
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}