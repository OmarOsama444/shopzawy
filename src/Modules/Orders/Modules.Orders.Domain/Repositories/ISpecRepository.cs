using Modules.Orders.Domain.Entities;
using Modules.Orders.Domain.ValueObjects;
using Modules.Users.Application.Abstractions;

namespace Modules.Orders.Domain.Repositories;

public interface ISpecRepository : IRepository<Specification>
{
    public Task<ICollection<Specification>> GetByDataType(string dataTypeName);
    public Task<ICollection<SpecResponse>> Paginate(int pageNumber, int pageSize, string? name, Language lang_code);
    public Task<int> Total(string? name, Language lang_code);
}

public class SpecResponse
{
    public Guid id { get; set; }
    public string name { get; set; } = string.Empty;
    public string dataType { get; set; } = string.Empty;
    public ICollection<SpecOptionsResponse> Options { get; set; } = [];
    // public SpecResponse(Guid id, string name, string dataType, ICollection<SpecOptionsResponse> Options)
    // {
    //     this.id = id;
    //     this.name = name;
    //     this.dataType = dataType;
    //     this.Options = Options;
    // }
}
public record SpecOptionsResponse(Guid option_id, string value);