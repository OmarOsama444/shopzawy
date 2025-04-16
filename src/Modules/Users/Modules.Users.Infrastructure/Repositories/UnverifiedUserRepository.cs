using System.Data.Common;
using Dapper;
using Modules.Users.Application.Abstractions;
using Modules.Users.Domain.Entities;
using Modules.Users.Domain.Repositories;

namespace Modules.Users.Infrastructure.Repositories;

public class UnverifiedUserRepository(UserDbContext userDbContext) :
    Repository<UnverifiedUser, UserDbContext>(userDbContext),
    IUnverifiedUserRepository
{

}
