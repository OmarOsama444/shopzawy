namespace Modules.Users.Application.UseCases.Dtos;

public record PermissionResponse(Guid Id, string Name, bool Active, string? Module);
