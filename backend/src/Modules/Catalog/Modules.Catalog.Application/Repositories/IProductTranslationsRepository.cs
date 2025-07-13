using Common.Domain;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.Repositories;

public interface IProductTranslationsRepository : IRepository<ProductTranslation>, ITranslationRepository<ProductTranslation, Guid>
{
}
