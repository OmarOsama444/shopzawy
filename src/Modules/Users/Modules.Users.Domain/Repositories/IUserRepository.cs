using Modules.Users.Application.Abstractions;

namespace Modules.Users.Domain;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByEmail(string Email);
}
