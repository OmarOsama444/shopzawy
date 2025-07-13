using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Exceptions;

public class BrandTranslationNotFoundException : NotFoundException
{
    public BrandTranslationNotFoundException(Guid id, Language lang_code) : base("Brand.Translation.NotFound", $"Translation for brand with id {id} for language {lang_code.ToString()} not found")
    {

    }
}
