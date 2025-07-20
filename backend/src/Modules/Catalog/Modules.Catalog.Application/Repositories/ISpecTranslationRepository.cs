using Common.Application;
using Common.Domain.ValueObjects;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.Repositories;

public interface ISpecTranslationRepository : IRepository<SpecificationTranslation>
{
    public Task<SpecificationTranslation?> GetBySpecIdAndLanguage(Guid specId, Language language);
}
