using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Exceptions;

public class CategoryTranslationNotFound : NotFoundException
{
    public CategoryTranslationNotFound(int id, Language language) : base(
        "Category.Translation.NotFound",
        $"category translation for category with id : {id} for language with code {language.ToString()}")
    {
    }
}