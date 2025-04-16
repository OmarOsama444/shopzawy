using Modules.Common.Application.Messaging;
using Modules.Users.Domain;
using Modules.Users.Domain.Exceptions;

namespace Modules.Users.Application.UseCases.GetUserById;

/// <summary>
/// <exception cref="UserNotFound"></exception>
/// </summary>
/// <param name="id"></param>
public sealed record GetUserByIdQuery(Guid id) : IQuery<User>, ICachedQuery
{
    public string Key => UserCachekey.UserId(id);
    public TimeSpan? Expiration => TimeSpan.FromMinutes(10);
}
