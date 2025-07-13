using Common.Domain;
using Modules.Catalog.Domain.Entities.Translation;

namespace Modules.Catalog.Application.Repositories;

public interface IBrandTranslationRepository : IRepository<BrandTranslation>, ITranslationRepository<BrandTranslation, Guid>;
