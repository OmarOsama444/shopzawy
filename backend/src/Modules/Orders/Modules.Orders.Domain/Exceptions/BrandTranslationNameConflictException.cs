using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Modules.Orders.Domain.Exceptions;

public class BrandTranslationNameConflictException : NotFoundException
{
    public BrandTranslationNameConflictException(Guid id, Language lang_code) : base("Brand.Translation.Conflict", $"Translation for brand with id {id} for language {lang_code.ToString()} already exists")
    {

    }
}