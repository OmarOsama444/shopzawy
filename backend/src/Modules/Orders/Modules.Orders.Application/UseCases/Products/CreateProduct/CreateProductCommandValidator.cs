using Common.Application.Validators;
using FluentValidation;

namespace Modules.Orders.Application.UseCases.Products.CreateProduct;

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.ProductNames)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.ProductNames.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.LongDescriptions)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.LongDescriptions.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.ShortDescriptions)
            .NotEmpty()
            .Must(LanguageValidator.Must)
            .WithMessage(LanguageValidator.Message);

        RuleForEach(c => c.ShortDescriptions.Values)
            .NotEmpty()
            .MinimumLength(10);

        RuleFor(c => c.WeightUnit).NotEmpty();
        RuleFor(c => c.DimensionUnit).NotEmpty();
        RuleFor(c => c.ProductItems).NotEmpty();
        RuleForEach(c => c.ProductItems)
            .SetValidator(new productItemValidator());

        RuleFor(x => x)
        .Must(HaveConsistentLanguageKeys)
        .WithMessage("All localized fields must have the same set of language codes (keys).");
    }
    private bool HaveConsistentLanguageKeys(CreateProductCommand cmd)
    {
        var keys1 = cmd.ProductNames?.Keys?.OrderBy(k => k).ToArray();
        var keys2 = cmd.LongDescriptions?.Keys?.OrderBy(k => k).ToArray();
        var keys3 = cmd.ShortDescriptions?.Keys?.OrderBy(k => k).ToArray();

        if (keys1 == null || keys2 == null || keys3 == null)
            return false;

        return keys1.SequenceEqual(keys2) && keys1.SequenceEqual(keys3);
    }
}
