using Modules.Orders.Domain.Entities;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ISpecRepository : IRepository<Specification>
{
    public Task<ICollection<Specification>> GetByDataType(string dataTypeName);
    public Task<ICollection<SpecResponse>> Paginate(int pageNumber, int pageSize, string? name);

    public Task<int> Total(string? name);
}

public record SpecResponse(Guid id, string name, string dataType, ICollection<SpecOptionsResponse> Options);
public record SpecOptionsResponse(Guid id, string value);