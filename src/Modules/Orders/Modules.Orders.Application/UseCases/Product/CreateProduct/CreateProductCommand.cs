using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Services;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.UseCases.CreateProduct;

public record CreateProductCommand(
    string product_name,
    string long_description,
    string short_description,
    WeightUnit weight_unit,
    float weight,
    DimensionUnit dimension_unit,
    float width,
    float length,
    float height,
    ICollection<string> tags,
    Guid vendor_id,
    string brand_name,
    Guid category_id,
    ICollection<product_item> product_items) : ICommand<Guid>;

public sealed class CreateProductCommandHandler(
    IProductService productService) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {

        var result = await productService.CreateProductWithItem(
            request.product_name,
            request.long_description,
            request.short_description,
            request.weight_unit,
            request.weight,
            request.dimension_unit,
            request.weight,
            request.length,
            request.height,
            request.tags,
            request.vendor_id,
            request.brand_name,
            request.category_id,
            request.product_items);

        return result;
    }
}

internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.product_name).NotEmpty();
        RuleFor(c => c.long_description).NotEmpty().MinimumLength(10);
        RuleFor(c => c.short_description).NotEmpty().MinimumLength(10);
        RuleFor(c => c.weight_unit).NotEmpty();
        RuleFor(c => c.weight).NotEmpty();
        RuleFor(c => c.dimension_unit).NotEmpty();
        RuleFor(c => c.width).NotEmpty();
        RuleFor(c => c.height).NotEmpty();
        RuleFor(c => c.length).NotEmpty();
        RuleFor(c => c.product_items).NotEmpty();
        RuleForEach(c => c.product_items)
            .SetValidator(new productItemValidator());
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
        RuleForEach(x => x.urls)
            .NotEmpty()
            .WithMessage("Urls Can't be empty")
            .Must(UrlValidator.Must)
            .WithMessage(UrlValidator.Message);
    }
}