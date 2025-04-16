using MassTransit.Contracts;
using Modules.Common.Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public class UserCreatedIntegrationEvent(Guid id, DateTime creationDate) : IntegrationEvent(id, creationDate)
{
    public Guid UserId { get; private set; } = Guid.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Email { get; private set; } = string.Empty;
    public string? PhoneNumber { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public static UserCreatedIntegrationEvent Create(
        Guid id,
        DateTime CreatedOn,
        Guid UserId,
        string FirstName,
        string LastName,
        string? Email,
        string? PhoneNumber,
        string Role
         )
    {
        return new UserCreatedIntegrationEvent(id, CreatedOn)
        {
            UserId = UserId,
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Role = Role
        };
    }
}
