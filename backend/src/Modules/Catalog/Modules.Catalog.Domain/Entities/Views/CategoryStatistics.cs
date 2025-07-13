using Common.Domain.Entities;

namespace Modules.Catalog.Domain.Entities.Views;

public class CategoryStatistics : Entity
{
    public Guid Id { get; private set; }
    public int Order { get; set; }
    public Guid? ParentCategoryId { get; private set; }
    public List<Guid> ChildCategoryIds { get; private set; } = [];
    public int TotalChildren { get; private set; }
    public int TotalProducts { get; private set; }
    public int TotalSpecs { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public static CategoryStatistics Create(
        Guid id,
        Guid? parentCategoryId,
        List<Guid> childCategoryIds,
        int totalChildren,
        int totalProducts,
        int totalSpecs,
        int order,
        DateTime createdOn)
    {
        return new CategoryStatistics
        {
            Id = id,
            ParentCategoryId = parentCategoryId,
            ChildCategoryIds = childCategoryIds ?? [],
            TotalChildren = totalChildren,
            TotalProducts = totalProducts,
            TotalSpecs = totalSpecs,
            Order = order,
            CreatedOn = createdOn
        };
    }

}
