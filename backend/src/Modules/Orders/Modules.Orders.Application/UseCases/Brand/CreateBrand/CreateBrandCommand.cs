using Common.Application.Messaging;
using Common.Application.Validators;
using Common.Domain;
using FluentValidation;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.Exceptions;

namespace Modules.Orders.Application.UseCases.CreateBrand;

public record CreateBrandCommand(
    string logoUrl,
    bool? featured,
    bool? active) : ICommand<Guid>;

public class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, Guid>
{
    private readonly IBrandRepository _brandRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    {
        _brandRepository = brandRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(request.logoUrl, request.featured, request.active);
        _brandRepository.Add(brand);
        await _unitOfWork.SaveChangesAsync();
        return brand.Id;
    }
}

internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.logoUrl).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
    }
}