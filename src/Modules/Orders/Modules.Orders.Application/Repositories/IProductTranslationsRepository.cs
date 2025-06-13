using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Application.Repositories;

public interface IProductTranslationsRepository : IRepository<ProductTranslation>, ITranslationRepository<ProductTranslation, Guid>
{
}
