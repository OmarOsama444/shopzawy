using Common.Application;
using Modules.Catalog.Domain.Entities;

namespace Modules.Catalog.Application.Repositories;

public interface IColorRepository : IRepository<Color>
{
    public Task<Color?> GetByName(string name);
    public Task<ICollection<ColorResponse>> Paginate(int pageSize, int pageNumber, string? name);
    public Task<int> TotalColors(string? name);
}

public class ColorResponse
{
    public string name { get; set; } = string.Empty;
    public string code { get; set; } = string.Empty;
};
