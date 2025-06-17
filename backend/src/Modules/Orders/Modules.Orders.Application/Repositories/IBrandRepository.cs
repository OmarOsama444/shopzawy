using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Orders.Application.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    public Task<ICollection<BrandResponse>> Paginate(int pageNumber, int pageSize, string? nameField, Language langCode);
    public Task<int> TotalBrands(string? namefiler);
}

public class BrandResponse
{
    public string BrandName { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public bool Active { get; set; }
    public int NumberOfProducts { get; set; }
    public BrandResponse()
    {

    }
    public BrandResponse(
        string brandName,
        string logoUrl,
        string description = "",
        bool featured = false,
        bool active = false,
        int numberOfProducts = 0)
    {
        BrandName = brandName;
        LogoUrl = logoUrl;
        Description = description;
        Featured = featured;
        Active = active;
        NumberOfProducts = numberOfProducts;
    }
}