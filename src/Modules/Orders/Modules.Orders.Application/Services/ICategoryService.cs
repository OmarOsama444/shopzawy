using Modules.Common.Domain;
using Modules.Orders.Application.UseCases.UpdateCategory;
using Modules.Orders.Domain.Repositories;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface ICategoryService
{
    public Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, string> names,
        IDictionary<Language, string> descriptions,
        IDictionary<Language, string> imageUrls
    );

}

public class CategoryRespone
{
    public Guid Id { get; set; } = Guid.Empty;
    public string categoryName { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int order { get; set; }
    public string? imageUrl { get; set; }
    public IDictionary<Guid, string> categoryPath { get; set; } = new Dictionary<Guid, string>();
    public ICollection<SpecResponse> specifications { get; set; } = [];
    public SubCategory? parent { get; init; }
    public ICollection<SubCategory> children { get; set; } = [];
    public class SubCategory
    {
        public Guid Id;
        public string categoryName { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public int order { get; set; }
        public string? imageUrl { get; set; }
    }
}
