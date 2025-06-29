using Common.Domain;
using Common.Domain.ValueObjects;
using Modules.Orders.Domain.Entities;

namespace Modules.Orders.Application.Repositories;

public interface IBrandRepository : IRepository<Brand>
{
    public Task<ICollection<TranslatedBrandResponseDto>> Paginate(int pageNumber, int pageSize, string? nameField, Language langCode);
    public Task<int> TotalBrands(string? namefiler, Language langCode);
}

public class TranslatedBrandResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool Featured { get; set; }
    public bool Active { get; set; }

}