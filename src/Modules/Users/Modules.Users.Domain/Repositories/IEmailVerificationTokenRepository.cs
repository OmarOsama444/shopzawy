using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;

namespace Modules.Users.Domain.Repositories;

public interface IEmailVerificationTokenRepository : IRepository<EmailVerificationToken>
{
    public Task<EmailVerificationToken?> GetByUserId(Guid id);
}
