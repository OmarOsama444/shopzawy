namespace Modules.Users.Application.Services;

public interface IEmailService
{
    public Task SendVerificationToken(string FirstName, string Email, string Token);
}
