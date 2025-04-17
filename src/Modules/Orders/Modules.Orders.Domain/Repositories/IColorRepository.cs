using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface IColorRepository : IRepository<Color>
{
    public Task<Color?> GetByName(string name);
    public Task<ICollection<ColorResponse>> Paginate(int pageSize, int pageNumber, string? name);
    public Task<int> TotalColors(string? name);
}

public record ColorResponse(string name, string code);
