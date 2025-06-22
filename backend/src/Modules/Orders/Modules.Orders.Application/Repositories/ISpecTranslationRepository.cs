using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface ISpecTranslationRepository : IRepository<SpecificationTranslation>
{
    public Task<SpecificationTranslation?> GetBySpecIdAndLanguage(Guid specId, Language language);
}
