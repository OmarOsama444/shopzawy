
using Common.Application.Messaging;
using Common.Application.Validators;
using Common.Domain;
using Common.Domain.ValueObjects;
using FluentValidation;
using Modules.Catalog.Application.Abstractions;
using Modules.Catalog.Application.Repositories;
using Modules.Catalog.Domain.Entities;
using Modules.Catalog.Domain.Entities.Translation;
using Modules.Catalog.Domain.Exceptions;

namespace Modules.Catalog.Application.UseCases.Brands.UpdateBrand;

public record UpdateBrandCommand(
    Guid BrandId,
    string? LogoUrl,
    bool? Featured,
    bool? Active,
    IDictionary<Language, string> Names,
    IDictionary<Language, string> Descriptions,
    IDictionary<Language, string> AddNames,
    IDictionary<Language, string> AddDescriptions,
    ICollection<Language> Remove
    ) : ICommand;

public sealed class UpdateBrandCommandHandler(IBrandRepository brandRepository, IBrandTranslationRepository brandTranslationRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateBrandCommand>
{
    public async Task<Result> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        Brand? brand = await brandRepository.GetByIdAsync(request.BrandId);
        if (brand is null)
            return new BrandNotFoundException(request.BrandId);
        brand.Update(request.LogoUrl, request.Featured, request.Active);
        brandRepository.Update(brand);

        foreach (Language key in request.Names.Keys.Union(request.Descriptions.Keys))
        {
            BrandTranslation? brandTranslation = await brandTranslationRepository.GetByIdAndLang(request.BrandId, key);
            if (brandTranslation is null)
                return new BrandTranslationNotFoundException(request.BrandId, key);
            string? name = request.Names.TryGetValue(key, out string? n) ? n : n;
            string? description = request.Descriptions.TryGetValue(key, out string? d) ? d : d;
            brandTranslation.Update(name, description);
            brandTranslationRepository.Update(brandTranslation);
        }

        foreach (Language key in request.AddNames.Keys)
        {
            BrandTranslation? brandTranslation = await brandTranslationRepository.GetByIdAndLang(request.BrandId, key);
            if (brandTranslation is not null)
                return new BrandTranslationNameConflictException(request.BrandId, key);
            string name = request.Names[key];
            string description = request.Descriptions[key];

            brandTranslation = BrandTranslation.Create(request.BrandId, key, name, description);
            brandTranslationRepository.Add(brandTranslation);
        }

        foreach (Language key in request.Remove)
        {
            BrandTranslation? brandTranslation = await brandTranslationRepository.GetByIdAndLang(request.BrandId, key);
            if (brandTranslation is null)
                continue;
            brandTranslationRepository.Remove(brandTranslation);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
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