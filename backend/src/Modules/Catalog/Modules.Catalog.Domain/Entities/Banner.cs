using Common.Domain.Entities;
using Modules.Catalog.Domain.ValueObjects;

namespace Modules.Catalog.Domain.Entities;

public class Banner : Entity
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Link { get; private set; } = string.Empty;
    public bool Active { get; private set; } = false;
    public BannerPosition Position { get; private set; }
    public BannerSize Size { get; private set; }
    public static Banner Create(
        string Title,
        string description,
        string Link,
        BannerPosition Position,
        BannerSize Size,
        bool Active)
    {
        return new Banner()
        {
            Title = Title,
            Description = description,
            Link = Link,
            Position = Position,
            Size = Size,
            Active = Active
        };
    }
}
