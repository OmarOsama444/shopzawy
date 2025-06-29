using Common.Application.Messaging;
using Common.Application.Validators;
using Common.Domain;
using Common.Domain.ValueObjects;
using FluentValidation;
using Modules.Orders.Application.Abstractions;
using Modules.Orders.Application.Repositories;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.UseCases.CreateBrand;

public record CreateBrandCommand(
    string LogoUrl,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    bool Featured = false,
    bool Active = true
    ) : ICommand<Guid>;

public class CreateBrandCommandHandler(
    IBrandTranslationRepository brandTranslationRepository,
    IBrandRepository brandRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateBrandCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(request.LogoUrl, request.Featured, request.Active);
        brandRepository.Add(brand);
        foreach (var key in request.Names.Keys)
        {
            var brandTranslation = BrandTranslation
                .Create(brand.Id, key, request.Names[key], request.Descriptions[key]);
            brandTranslationRepository.Add(brandTranslation);
        }
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return brand.Id;
    }
}

internal class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateBrandCommandValidator()
    {
        RuleFor(b => b.LogoUrl).NotEmpty().Must(UrlValidator.Must).WithMessage(UrlValidator.Message);
        RuleForEach(x => x.Names.Values).NotEmpty().MinimumLength(3).MaximumLength(100);
        RuleForEach(x => x.Descriptions.Values).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Names).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(x => x.Descriptions).Must(LanguageValidator.Must).WithMessage(LanguageValidator.Message);
        RuleFor(x => x).Must(x => ConsistentKeysValidator.Must(x.Names, x.Descriptions)).WithMessage(ConsistentKeysValidator.Message);
    }
}