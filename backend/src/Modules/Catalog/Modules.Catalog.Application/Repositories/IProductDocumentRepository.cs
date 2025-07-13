using Modules.Catalog.Domain.Elastic;

namespace Modules.Catalog.Application.Repositories;

public interface IProductDocumentRepository
{
    public Task Add(ProductDocument productDocument);
    public Task Delete(Guid Id);
    public Task Update(ProductDocument productDocument);
}