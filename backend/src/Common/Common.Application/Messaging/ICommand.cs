using Common.Domain;
using MediatR;


namespace Common.Application.Messaging
{
    public interface ICommand : IRequest<Result>, IBaseCommand;
    public interface ICommand<TValue> : IRequest<Result<TValue>>, IBaseCommand;
    public interface IBaseCommand;
}
