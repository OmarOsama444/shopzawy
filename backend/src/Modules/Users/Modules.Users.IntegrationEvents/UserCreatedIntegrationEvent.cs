using Common.Application.EventBus;

namespace Modules.Users.IntegrationEvents;

public class UserCreatedIntegrationEvent : IntegrationEvent
{
    public Guid Id { get; set; }
    public Guid GuestId { get; set; }
    public string? LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string? CountryCode { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; } = false;
    public static UserCreatedIntegrationEvent Create(
        Guid id,
        Guid guestId,
        string? lastName,
        string firstName,
        string? countryCode,
        string? email,
        string? phoneNumber,
        bool EmailConfirmed = false,
        bool phoneNumberConfirmed = false)
    {
        return new UserCreatedIntegrationEvent
        {
            Id = id,
            GuestId = guestId,
            LastName = lastName,
            FirstName = firstName,
            CountryCode = countryCode,
            Email = email,
            PhoneNumber = phoneNumber,
            PhoneNumberConfirmed = phoneNumberConfirmed,
            EmailConfirmed = EmailConfirmed
        };
    }
}
