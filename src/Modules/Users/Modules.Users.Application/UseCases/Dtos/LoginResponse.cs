namespace Modules.Users.Application.UseCases.Dtos
{
    public record LoginResponse(string refreshToken, string accessToken);
}
