using Common.Domain.ValueObjects;

namespace Modules.Catalog.Application.Repositories;

public interface ITranslationRepository<T, Tid>
{
    public Task<T?> GetByIdAndLang(Tid id, Language langCode);
}
