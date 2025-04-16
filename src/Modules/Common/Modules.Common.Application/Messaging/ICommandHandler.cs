using MediatR;
using Modules.Common.Domain;


namespace Modules.Common.Application.Messaging;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand , Result>
    where TCommand : ICommand;
public interface ICommandHandler<TCommand , TResponse> : IRequestHandler<TCommand , Result<TResponse>> 
    where TCommand : ICommand<TResponse>;

