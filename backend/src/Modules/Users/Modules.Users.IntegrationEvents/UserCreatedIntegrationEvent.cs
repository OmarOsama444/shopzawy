using Common.Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public class UserCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? Email { get; set; }
    public static UserCreatedIntegrationEvent Create(
        Guid id,
        string firstName,
        string? lastName,
        string? email)
    {
        return new UserCreatedIntegrationEvent
        {
            Id = id,
            LastName = lastName,
            FirstName = firstName,
            Email = email,
        };
    }
}
