using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Services;

public interface IUserService
{
    public Task<LoginResponse> LoginUser(User user, CancellationToken cancellationToken = default);
    public Task<LoginResponse> LoginGuest(Guid GuestId, CancellationToken cancellationToken = default);
    public Task<User> RegisterUser(User user, CancellationToken cancellationToken = default);
}
