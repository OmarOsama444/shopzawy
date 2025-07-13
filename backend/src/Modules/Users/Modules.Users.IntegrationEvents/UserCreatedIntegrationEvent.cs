using Common.Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public class UserCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? CountryCode { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public static UserCreatedIntegrationEvent Create(
        Guid id,
        string? lastName,
        string firstName,
        string? countryCode,
        string? email,
        string? phoneNumber)
    {
        return new UserCreatedIntegrationEvent
        {
            Id = id,
            LastName = lastName,
            FirstName = firstName,
            CountryCode = countryCode,
            Email = email,
            PhoneNumber = phoneNumber
        };
    }
}
