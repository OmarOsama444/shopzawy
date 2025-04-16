using Modules.Common.Domain.Entities;
using Modules.Users.Domain.DomainEvents;

namespace Modules.Users.Domain.Entities;

public class SmsVerificationToken : Entity
{
    public Guid Id { get; set; }
    public int token { get; set; }
    public Guid UserId { get; private set; }
    public virtual UnverifiedUser? User { get; private set; }
    public DateTime CreatedOnUtc { get; private set; }
    public DateTime ExpiresOnUtc { get; private set; }
    public static SmsVerificationToken Create(Guid userId, int LifeTimeInMinutes = 3)
    {
        var random = new Random();
        var smsToken = new SmsVerificationToken()
        {
            UserId = userId,
            Id = Guid.NewGuid(),
            token = random.Next(1000, 10000),
            CreatedOnUtc = DateTime.Now,
            ExpiresOnUtc = DateTime.Now.AddMinutes(LifeTimeInMinutes)
        };
        smsToken.RaiseDomainEvent(new SmsTokenCreatedDomainEvent(smsToken.Id));
        return smsToken;
    }

}
