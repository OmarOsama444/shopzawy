using Common.Application.Messaging;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Dtos;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.ValueObjects;


namespace Modules.Orders.Application.UseCases.ProductItems.CreateProductItem;

public sealed class CreateProductItemCommandHandler(
    IProductRepository productRepository,
    ISpecRepository specRepository,
    ICategoryRepository categoryRepository,
    ISpecOptionRepository specOptionRepository,
    IProductItemRepository productItemRepository,
    IProductItemOptionColorRepository productItemOptionColorRepository,
    IProductItemOptionNumericRepository productItemOptionNumericRepository,
    IProductItemOptionsRepository productItemOptionsRepository,
    IColorRepository colorRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateProductItemCommand, ICollection<Guid>>
{
    public async Task<Result<ICollection<Guid>>> Handle(CreateProductItemCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId);
        if (product is null)
            return new ProductNotFoundException(request.ProductId);

        var category = await categoryRepository.GetByIdAsync(product.CategoryId);
        if (category is null)
            return new CategoryNotFoundException(product.CategoryId);

        var categoryPath = category.Path.Concat([category.Id]).ToArray();

        var specifications = await specRepository.GetByCategoryId(category.Id, categoryPath, Language.en);
        var resultIds = new List<Guid>();

        foreach (var dto in request.ProductItems)
        {
            if (await productItemRepository.GetByProductIdAndSku(request.ProductId, dto.StockKeepingUnit) is not null)
                return new ProductItemConflictException(dto.StockKeepingUnit);

            var productItem = ProductItem.Create(
                dto.StockKeepingUnit, dto.QuantityInStock, dto.Price,
                dto.Width, dto.Length, dto.Height, dto.Weight,
                product.Id, dto.Urls
            );

            productItemRepository.Add(productItem);

            if (!await HandleStringOptionsAsync(productItem, dto.StringOptions, specifications))
                return new NotFoundException("Spec.NotFound", $"Invalid String specification");

            if (!HandleNumericOptions(productItem, dto.NumericOptions, specifications))
                return new NotFoundException("Spec.NotFound", $"Invalid Numeric specification");

            if (!await HandleColorOptionsAsync(productItem, dto.ColorOptions, specifications))
                return new NotFoundException("Spec.NotFound", $"Invalid Numeric specification");

            await unitOfWork.SaveChangesAsync(cancellationToken);
            resultIds.Add(productItem.Id);
        }

        return resultIds;
    }

    private async Task<bool> HandleStringOptionsAsync(ProductItem productItem, IDictionary<Guid, string> options, IEnumerable<TranslatedSpecResponseDto> specs)
    {
        foreach (var (specId, value) in options)
        {
            var spec = specs.FirstOrDefault(x => x.Id == specId && x.DataType == SpecDataType.String);
            if (spec is null || await specOptionRepository.GetBySpecIdAndValue(specId, value) is null)
                return false;

            var option = ProductItemOptions.Create(productItem.Id, specId, value);
            productItemOptionsRepository.Add(option);
        }
        return true;
    }

    private bool HandleNumericOptions(ProductItem productItem, IDictionary<Guid, float> options, IEnumerable<TranslatedSpecResponseDto> specs)
    {
        foreach (var (specId, value) in options)
        {
            var spec = specs.FirstOrDefault(x => x.Id == specId && x.DataType == SpecDataType.Number);
            if (spec is null)
                return false;

            var option = ProductItemOptionNumeric.Create(productItem.Id, specId, value);
            productItemOptionNumericRepository.Add(option);
        }
        return true;
    }

    private async Task<bool> HandleColorOptionsAsync(ProductItem productItem, IDictionary<Guid, string> options, IEnumerable<TranslatedSpecResponseDto> specs)
    {
        foreach (var (specId, colorCode) in options)
        {
            if (await colorRepository.GetByIdAsync(colorCode) is null)
                return false;

            var spec = specs.FirstOrDefault(x => x.Id == specId && x.DataType == SpecDataType.Color);
            if (spec is null)
                return false;

            var option = ProductItemOptionColor.Create(productItem.Id, specId, colorCode);
            productItemOptionColorRepository.Add(option);
        }
        return true;
    }
}
