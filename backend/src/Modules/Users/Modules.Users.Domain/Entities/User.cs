
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
    public string? Email { get; set; }
    public bool EmailConfirmed { get; set; } = false;
    public string PasswordHash { get; set; } = string.Empty;
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
        string FirstName,
        string LastName,
        string? Email)
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
            DateOfCreation = DateTime.UtcNow
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
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

}
