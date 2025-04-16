using Modules.Common.Domain.Entities;
using Modules.Users.Domain.DomainEvents;

namespace Modules.Users.Domain.Entities;

public class EmailVerificationToken : Entity
{
    public string Id { get; set; } = string.Empty;
    public Guid UserId { get; private set; }
    public virtual UnverifiedUser? User { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }
    public static EmailVerificationToken Create(Guid userId, int LifeTimeInDays = 1)
    {
        var emailToken = new EmailVerificationToken()
        {
            UserId = userId,
            Id = Guid.NewGuid().ToString(),
            CreatedOnUtc = DateTime.Now,
            ExpiresOnUtc = DateTime.Now.AddDays(LifeTimeInDays)
        };
        emailToken.RaiseDomainEvent(new EmailTokenCreatedDomainEvent(emailToken.Id));
        return emailToken;
    }

}
