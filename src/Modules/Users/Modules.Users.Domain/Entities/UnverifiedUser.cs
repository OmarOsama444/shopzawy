using System.Security.Cryptography.X509Certificates;
using Modules.Common.Domain.Entities;
using Modules.Users.Domain.DomainEvents;

namespace Modules.Users.Domain.Entities;

public class UnverifiedUser : Entity
{
    private UnverifiedUser() { }
    public Guid Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public DateTime DateOfCreation { get; private set; }
    public static UnverifiedUser Create(string FirstName, string LastName, string HashedPassword, string Role, string Email, string PhoneNumber)
    {
        var user = new UnverifiedUser()
        {
            Id = Guid.NewGuid(),
            FirstName = FirstName,
            LastName = LastName,
            HashedPassword = HashedPassword,
            Email = Email,
            PhoneNumber = PhoneNumber,
            DateOfCreation = DateTime.Now,
            Role = Role
        };
        user.RaiseDomainEvent(new UnverifiedUserCreatedDomainEvent(user.Id));
        return user;
    }
}
