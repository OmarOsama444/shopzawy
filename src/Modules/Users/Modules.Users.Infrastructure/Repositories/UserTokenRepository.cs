using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;
using Modules.Users.Domain.ValueObjects;

namespace Modules.Users.Infrastructure.Repositories;

public class UserTokenRepository(UserDbContext context) : IUserTokenRepository
{
    public void Add(UserToken userToken)
    {
        context.userTokens.Add(userToken);
    }


    public async Task<UserToken?> GetToken(string value, TokenType tokenType)
    {
        return await context
            .userTokens
            .FirstOrDefaultAsync(
                x => x.Value == value &&
                x.tokenType == tokenType
            );
    }

}
