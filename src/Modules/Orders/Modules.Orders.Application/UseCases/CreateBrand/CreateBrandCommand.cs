using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;
using Modules.Orders.Domain.Repositories;

namespace Modules.Orders.Application.UseCases.CreateBrand;

public record CreateBrandCommand(
    string brandName,
    string logoUrl,
    string description,
    bool? featured,
    bool? active) : ICommand<string>;

public class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, string>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        // Check if the brand already exists
        var existingBrand = await _brandRepository.GetByIdAsync(request.brandName);
        if (existingBrand != null)
            return new BrandConflictException(request.brandName);

        var brand = Brand.Create(request.brandName, request.logoUrl, request.description, request.featured, request.active);
        _brandRepository.Add(brand);
        await _unitOfWork.SaveChangesAsync();

        return brand.BrandName;
    }
}

internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.brandName).NotEmpty().MinimumLength(3);
        RuleFor(b => b.logoUrl).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleFor(b => b.description).NotEmpty().MinimumLength(3);
    }
}