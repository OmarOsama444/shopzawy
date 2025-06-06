using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand, string>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if ((await roleRepository.GetByIdAsync(request.RoleName)) is not null)
            return new RoleNameConflict(request.RoleName);
        var role = Role.Create(request.RoleName);
        roleRepository.Add(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Name;
    }
}
