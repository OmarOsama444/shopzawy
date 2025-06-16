using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Users.UpdateUserRoles;

public class UpdateUserRolesCommandHandler(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUserRoleRepository userRoleRepository
) : ICommandHandler<UpdateUserRolesCommand>
{
    public async Task<Result> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.Id);
        if (user is null)
            return new UserNotFound(request.Id);

        foreach (string roleId in request.RemoveRoles)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            if (role is null)
                return new RoleNotFound(roleId);
            UserRole? userRole = await userRoleRepository.GetByUserRoleId(user.Id, roleId);
            if (userRole is not null)
                userRoleRepository.Remove(userRole);
        }

        foreach (var roleId in request.AddRoles)
        {
            var role = await roleRepository.GetByIdAsync(roleId);
            if (role is null)
                return new RoleNotFound(roleId);
            UserRole? userRole = await userRoleRepository.GetByUserRoleId(user.Id, roleId);
            if (userRole is null)
                userRoleRepository.Add(new UserRole { UserId = user.Id, RoleId = roleId });
        }
        return Result.Success();
    }
}
