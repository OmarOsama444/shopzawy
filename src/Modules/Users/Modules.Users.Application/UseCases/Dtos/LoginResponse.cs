namespace Modules.Users.Application.UseCases.Dtos
{
    public record LoginResponse(string RefreshToken, string AccessToken);
}
