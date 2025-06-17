using Common.Domain;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Repositories;

public interface ICategoryTranslationRepository : IRepository<CategoryTranslation>, ITranslationRepository<CategoryTranslation, Guid>
{
}
