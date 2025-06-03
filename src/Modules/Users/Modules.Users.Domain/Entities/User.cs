
using Microsoft.AspNetCore.Identity;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain;

public class User : IdentityUser<Guid>
{
    public User() { }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime DateOfCreation { get; set; }
    public string ConfirmationToken { get; set; } = string.Empty;
    public virtual ICollection<UserToken> UserTokens { get; set; } = [];
    public virtual ICollection<UserRole> UserRoles { get; set; } = [];
    public static User Create(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber)
    {

        var user = new User()
        {
            Id = Guid.NewGuid()
        ,
            ConfirmationToken = Guid.NewGuid().ToString()
        ,
            FirstName = FirstName
        ,
            LastName = LastName
        ,
            Email = Email
        ,
            PhoneNumber = PhoneNumber
        ,
            DateOfCreation = DateTime.Now,
        };
        return user;
    }

}
