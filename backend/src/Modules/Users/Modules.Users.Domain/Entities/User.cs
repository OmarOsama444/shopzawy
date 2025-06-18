
using Common.Domain.Entities;

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
    public DateTime? LastLoginDate { get; set; } = null;
    public void UpdateLastLoginDate(Guid GuestId)
    {
        this.LastLoginDate = DateTime.UtcNow;
        this.RaiseDomainEvent(new UserLoggedDomainEvent(this.Id, GuestId));
    }
    public static User AdminSeed()
    {
        return new User()
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@gmail.com",
            EmailConfirmed = true,
            PasswordHash = "AQAAAAIAAYagAAAAEJOqYyDPiMJFm1mVQx3qEAyLF9qqYyRZQamJuHF11binnXBQGuCSBJu+8T4lDkxPxg=="
        };
    }
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
