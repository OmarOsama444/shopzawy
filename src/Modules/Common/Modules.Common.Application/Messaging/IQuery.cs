using MediatR;
using Modules.Common.Domain;


namespace Modules.Common.Application.Messaging
{
    public interface IQuery : IRequest<Result>; 
    public interface IQuery<TResult> : IRequest<Result<TResult>>; 
}
