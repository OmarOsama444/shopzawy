using MassTransit.Initializers;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Users.Application.UseCases.Dtos;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Application.UseCases.Roles.PaginateRoles;

public class PaginateRolesQueryHandler(
    IRoleRepository roleRepository
) : IQueryHandler<PaginateRolesQuery, PaginationResponse<RoleResponse>>
{
    public async Task<Result<PaginationResponse<RoleResponse>>> Handle(PaginateRolesQuery request, CancellationToken cancellationToken)
    {
        var data = (await roleRepository.Paginate(request.PageSize, request.PageNumber, request.Name)).Select(x => new RoleResponse(x.Id, x.Name, x.CreatedOnUtc)).ToList();
        var count = await roleRepository.Count(request.Name);
        return new PaginationResponse<RoleResponse>(data, count, request.PageSize, request.PageNumber);
    }
}
