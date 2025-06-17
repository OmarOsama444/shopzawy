using Common.Application.Messaging;
using Common.Domain;
using Modules.Users.Application.Repositories;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.Roles.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand, string>
{
    public async Task<Result<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (await roleRepository.GetByName(request.Name) is not null)
            return new RoleNameConflict(request.Name);
        var role = Role.Create(request.Name);
        roleRepository.Add(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Name;
    }
}
