
using System.Runtime.InteropServices;
using Modules.Common.Domain.DomainEvent;
using Modules.Common.Domain.Entities;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain.Entities;

public class User : Entity
{
    public User() { }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string? LastName { get; set; } = string.Empty;
    public DateTime DateOfCreation { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public string? CountryCode { get; set; }
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    public string? PhoneNumber { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public bool PhoneNumberConfirmed { get; set; } = false;
    public static User Create(
        Guid GuestId,
        string FirstName,
        string LastName,
        string? Email,
        string? PhoneNumber,
        string? CountryCode)
    {
        var user = new User()
        {
            Id = Guid.NewGuid()
        ,
            FirstName = FirstName
        ,
            LastName = LastName
        ,
            Email = Email
        ,
            EmailConfirmed = false
        ,
            PhoneNumber = PhoneNumber
        ,
            PhoneNumberConfirmed = false
        ,
            DateOfCreation = DateTime.UtcNow
        ,
            CountryCode = CountryCode
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, GuestId));
        return user;
    }

    public static User Create(Guid GuestId, string Email, string FirstName, string? LastName)
    {
        var user = new User()
        {
            Id = Guid.NewGuid()
                ,
            FirstName = FirstName
                ,
            LastName = LastName
                ,
            Email = Email
                ,
            EmailConfirmed = true
                ,
            DateOfCreation = DateTime.UtcNow
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id, GuestId));
        return user;
    }

    public void SetPassword(string PasswordHash)
    {
        this.PasswordHash = PasswordHash;
    }

    public void ConfirmEmail()
    {
        this.EmailConfirmed = true;
    }

    public void ConfirmPhoneNumber()
    {
        this.PhoneNumberConfirmed = true;
    }
}
