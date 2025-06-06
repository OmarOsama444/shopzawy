using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ICategoryTranslationRepository : IRepository<CategoryTranslation>, ITranslationRepository<CategoryTranslation, Guid>
{
}
