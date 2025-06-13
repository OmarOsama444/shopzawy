namespace Modules.Users.Application.UseCases.Dtos;

public record PermissionResponse(string Name, bool Active, string? Module);
