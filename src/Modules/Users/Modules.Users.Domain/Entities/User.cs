using Modules.Common.Domain.Entities;

namespace Modules.Users.Domain;

public class User : Entity
{
    private User() { }
    public Guid Id { get; set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Role { get; private set; } = string.Empty;
    public DateTime DateOfCreation { get; private set; }
    public bool VerifiedEmail { get; private set; }
    public bool VerifiedPhone { get; private set; }
    public static User Create(string FirstName, string LastName, string HashedPassword, string Role, string Email)
    {

        var user = new User()
        {
            Id = Guid.NewGuid()
        ,
            FirstName = FirstName
        ,
            LastName = LastName
        ,
            HashedPassword = HashedPassword
        ,
            Email = Email
        ,
            DateOfCreation = DateTime.Now
        ,
            Role = Role
        };
        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
        return user;
    }

    public void UpdatePassword(string newHashedPassword)
    {
        if (newHashedPassword == this.HashedPassword)
            return;
        this.HashedPassword = newHashedPassword;
    }

    public void UpdateName(string FirstName, string LastName)
    {
        if (FirstName == this.FirstName && LastName == this.LastName)
        {
            return;
        }
        if (FirstName is not null)
            this.FirstName = FirstName;
        if (LastName is not null)
            this.LastName = LastName;
    }
}
