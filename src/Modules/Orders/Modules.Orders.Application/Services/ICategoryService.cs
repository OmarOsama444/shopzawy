using Modules.Common.Domain;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Services;

public interface ICategoryService
{
    public Task<Result<Guid>> CreateCategory(
        int Order,
        Guid? parentCategoryId,
        ICollection<Guid> Ids,
        IDictionary<Language, CategoryLangData> categoryLangData
    );

}

public record CategoryLangData(string name, string description, string image_url);


public class CategoryRespone
{
    public Guid Id { get; set; } = Guid.Empty;
    public string categoryName { get; set; } = string.Empty;
    public string description { get; set; } = string.Empty;
    public int order { get; set; }
    public string? imageUrl { get; set; }
    public ICollection<string> categoryPath { get; set; } = [];
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
        public string categoryPath { get; set; } = string.Empty;
    }
    public class SpecResponse
    {
        public Guid id { get; set; }
        public string name { get; set; } = string.Empty;
        public string dataType { get; set; } = string.Empty;
        public ICollection<SpecOptionResponse> options { get; set; } = [];
    }
    public record SpecOptionResponse(Guid optionId, string value);
}
