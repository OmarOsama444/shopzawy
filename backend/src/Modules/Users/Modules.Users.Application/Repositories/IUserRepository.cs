using Common.Application;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Repositories;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByConfirmedEmail(string Email);
}
