using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Application.Repositories;

public interface IBrandTranslationRepository : IRepository<BrandTranslation>, ITranslationRepository<BrandTranslation, Guid>;
