using Common.Domain;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Application.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    public Task<UserRole?> GetByUserRoleId(Guid UserId, string RoleId);
}