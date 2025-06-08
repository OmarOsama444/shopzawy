using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Exceptions;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.Roles.CreateRole;

public class CreateRoleCommandHandler(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : ICommandHandler<CreateRoleCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        if (await roleRepository.GetByName(request.Name) is not null)
            return new RoleNameConflict(request.Name);
        var role = Role.Create(request.Name);
        roleRepository.Add(role);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return role.Id;
    }
}
