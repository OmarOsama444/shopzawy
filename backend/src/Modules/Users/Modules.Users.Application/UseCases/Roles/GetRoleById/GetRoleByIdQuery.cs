using MassTransit.SagaStateMachine;
using Modules.Common.Application.Messaging;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Roles.GetRoleById;

public record GetRoleByIdQuery(string name) : IQuery<RoleDetailResponse>;
