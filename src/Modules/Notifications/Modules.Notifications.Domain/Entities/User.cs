using Modules.Common.Domain.Entities;
using Modules.Notifications.Domain.DomainEvents;
using Modules.Notifications.Domain.ValueObjects;
namespace Modules.Notifications.Domain.Entities;
public class User : Entity
{
    private User() { }
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public double Balance { get; private set; } = 0.0;
    public static User Create(string FirstName, string LastName, string Role, string Email, string PhoneNumber)
    {
        var user = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            PhoneNumber = PhoneNumber,
            Balance = 0.0,
            Role = Role
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }
}
