using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByConfirmedEmail(string Email);
    public Task<User?> GetByConfirmedPhone(string PhoneNumber);
}
