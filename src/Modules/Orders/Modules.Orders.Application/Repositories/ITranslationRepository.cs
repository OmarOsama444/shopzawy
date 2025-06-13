using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Repositories;

public interface ITranslationRepository<T, Tid>
{
    public Task<T?> GetByIdAndLang(Tid id, Language langCode);
}
