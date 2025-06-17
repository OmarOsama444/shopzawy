using Common.Application.Messaging;
using MassTransit.SagaStateMachine;
using Modules.Users.Application.UseCases.Dtos;

namespace Modules.Users.Application.UseCases.Roles.GetRoleById;

public record GetRoleByIdQuery(string name) : IQuery<RoleDetailResponse>;
