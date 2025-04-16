using FluentValidation;
using Modules.Common.Application.Messaging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;
using Modules.Users.Domain;

namespace Modules.Users.Application.UseCases.GetUserByEmail;

public record GetUserByEmailQuery(string email) : IQuery<User>;

public class GetUserByEmailQueryHandler
    (IUserRepository userRepository) : IQueryHandler<GetUserByEmailQuery, User>
{
    public async Task<Result<User>> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetUserByEmail(request.email);
        if (user == null)
            return new NotFoundException("Email.NotFound", $"User with email {request.email} not found");

        return user;
    }
}


internal class GetUserByEmailQueryValidator : AbstractValidator<GetUserByEmailQuery>
{
    public GetUserByEmailQueryValidator()
    {
        RuleFor(e => e.email).EmailAddress();
    }
}