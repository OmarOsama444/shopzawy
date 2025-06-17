using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IBrandTranslationRepository : IRepository<BrandTranslation>, ITranslationRepository<BrandTranslation, Guid>;
