
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Modules.Common.Application.Messaging;
using Modules.Common.Application.Validators;
using Modules.Common.Domain;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.UpdateBrand;

public record UpdateBrandCommand(
    Guid BrandId,
    string? LogoUrl,
    string? Description,
    bool? Featured,
    bool? Active) : ICommand;

public sealed class UpdateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateBrandCommand>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandRepository.GetByIdAsync(request.BrandId);
        if (brand is null)
            return new BrandNotFoundException(request.BrandId);
        brand.Update(request.Description, request.LogoUrl, request.Featured, request.Active);
        brandRepository.Update(brand);
        await unitOfWork.SaveChangesAsync();
        return Result.Success();
    }
}

internal class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
{
    public UpdateBrandCommandValidator()
    {
        RuleFor(b => b.BrandId).NotEmpty();
        RuleFor(b => b.LogoUrl!).Must(UrlValidator.Must).WithMessage(UrlValidator.Message).When(b => !String.IsNullOrEmpty(b.LogoUrl));
    }
}