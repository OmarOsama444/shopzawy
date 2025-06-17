using Common.Domain.Exceptions;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Domain.Exceptions;

public class CategoryTranslationNotFound : NotFoundException
{
    public CategoryTranslationNotFound(Guid id, Language language) : base(
        "Category.Translation.NotFound",
        $"category translation for category with id : {id} for language with code {language.ToString()}")
    {
    }
}