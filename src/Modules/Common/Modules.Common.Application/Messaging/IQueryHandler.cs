using MediatR;
using Modules.Common.Domain;

namespace Modules.Common.Application.Messaging;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery;

public interface IQueryHandler<TQuery,TResult> : IRequestHandler<TQuery , Result<TResult>> 
    where TQuery : IQuery<TResult>; 

