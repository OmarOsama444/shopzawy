using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Services;

public interface IUserService
{
    public Task<LoginResponse> LoginUser(User user, CancellationToken cancellationToken = default);
    public Task<User> RegisterUser(User user, string password, CancellationToken cancellationToken = default);
}
