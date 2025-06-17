using Common.Domain;
using MediatR;

namespace Common.Application.Messaging;

public interface IQueryHandler<TQuery> : IRequestHandler<TQuery, Result>
    where TQuery : IQuery;

public interface IQueryHandler<TQuery, TResult> : IRequestHandler<TQuery, Result<TResult>>
    where TQuery : IQuery<TResult>;

