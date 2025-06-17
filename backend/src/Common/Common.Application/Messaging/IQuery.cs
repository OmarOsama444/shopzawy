using Common.Domain;
using MediatR;


namespace Common.Application.Messaging
{
    public interface IQuery : IRequest<Result>;
    public interface IQuery<TResult> : IRequest<Result<TResult>>;
}
