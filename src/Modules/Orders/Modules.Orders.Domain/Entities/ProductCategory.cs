namespace Modules.Orders.Domain.Entities;

public class ProductCategory
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public virtual Product Product { get; set; } = default!;
    public virtual Category Category { get; set; } = default!;
}