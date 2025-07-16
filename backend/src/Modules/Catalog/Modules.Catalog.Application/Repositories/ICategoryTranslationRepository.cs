using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Catalog.Domain.Entities.Translation;
namespace Modules.Catalog.Application.Repositories;

public interface ICategoryTranslationRepository : IRepository<CategoryTranslation>, ITranslationRepository<CategoryTranslation, int>
{
}
