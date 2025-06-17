using Common.Domain;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IProductTranslationsRepository : IRepository<ProductTranslation>, ITranslationRepository<ProductTranslation, Guid>
{
}
