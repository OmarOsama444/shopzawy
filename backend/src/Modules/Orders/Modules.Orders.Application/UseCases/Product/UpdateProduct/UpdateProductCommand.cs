using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using FluentValidation;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.UpdateProduct;

public record LocalizedText(IDictionary<Language, string> translations);
public record UpdateProductCommand(
    Guid product_id,
    LocalizedText product_names,
    LocalizedText long_descriptions,
    LocalizedText short_descriptions,
    WeightUnit? weight_unit,
    DimensionUnit? dimension_unit,
    ICollection<string>? tags) : ICommand<Guid>;

public sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IProductTranslationsRepository productTranslationsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.product_id);
        if (product is null)
            return new ProductNotFoundException(request.product_id);
        product.Update(
            request.weight_unit,
            request.dimension_unit,
            request.tags);
        var keys =
            request.product_names.translations.Keys
            .Union(request.long_descriptions.translations.Keys)
            .Union(request.short_descriptions.translations.Keys);
        foreach (Language langCode in keys)
        {
            var productTranslation = await productTranslationsRepository.GetByIdAndLang(request.product_id, langCode);
            if (productTranslation is null)
                return new ProductTranslationNotFound(request.product_id, langCode);
            productTranslation.Update(
                request.product_names.translations.TryGetValue(langCode, out string? name) ? name : null,
                request.long_descriptions.translations.TryGetValue(langCode, out string? long_description) ? long_description : null,
                request.short_descriptions.translations.TryGetValue(langCode, out string? short_description) ? short_description : null
                );
            productTranslationsRepository.Update(productTranslation);
        }

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}

internal class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.product_id).NotEmpty();
        RuleFor(c => c.weight_unit)
            .NotEmpty()
            .When(x => x.weight_unit != null);
        RuleFor(c => c.dimension_unit)
            .NotEmpty()
            .When(x => x.dimension_unit != null);
    }
}