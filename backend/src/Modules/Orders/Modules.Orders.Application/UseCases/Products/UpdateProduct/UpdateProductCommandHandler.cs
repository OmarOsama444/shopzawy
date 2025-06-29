using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IProductRepository productRepository,
    IProductTranslationsRepository productTranslationsRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return new ProductNotFoundException(request.ProductId);
        product.Update(
            request.WeightUnit,
            request.DimensionUnit,
            request.Tags);
        var keys =
            request.ProductNames.translations.Keys
            .Union(request.LongDescriptions.translations.Keys)
            .Union(request.ShortDescriptions.translations.Keys);
        foreach (Language langCode in keys)
        {
            var productTranslation = await productTranslationsRepository.GetByIdAndLang(request.ProductId, langCode);
            if (productTranslation is null)
                return new ProductTranslationNotFound(request.ProductId, langCode);
            productTranslation.Update(
                request.ProductNames.translations.TryGetValue(langCode, out string? name) ? name : null,
                request.LongDescriptions.translations.TryGetValue(langCode, out string? long_description) ? long_description : null,
                request.ShortDescriptions.translations.TryGetValue(langCode, out string? short_description) ? short_description : null
                );
            productTranslationsRepository.Update(productTranslation);
        }

        productRepository.Update(product);

        await unitOfWork.SaveChangesAsync();
        return product.Id;
    }
}
