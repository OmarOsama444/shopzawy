using Modules.Orders.Domain.Elastic;

namespace Modules.Orders.Application.Repositories
{
    public interface IProductDocumentRepository
    {
        public Task Add(ProductDocument productDocument);
        public Task Delete(Guid Id);
        public Task AddProductItem(Guid Id, ProductItemDocument productItemDocument);
        public Task RemoveProductItem(Guid Id, Guid ProductItemId);
    }
}