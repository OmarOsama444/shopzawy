namespace Modules.Orders.Application.Services.Dtos;

public record ProductItemDto(
        string StockKeepingUnit,
        int QuantityInStock,
        float Price,
        float Weight,
        float Width,
        float Length,
        float Height,
        ICollection<string> Urls,
        IDictionary<Guid, string> SpecOptions);
