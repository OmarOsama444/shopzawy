using MediatR;
using Modules.Common.Application.Caching;
using Modules.Common.Application.Messaging;

namespace Modules.Common.Application.Behaviour
{
    public class CachingPiplineBehaviour<TRequest, TResponse>(ICacheService cacheService) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : ICachedQuery
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var Response = await cacheService.GetAsync<TResponse>(request.Key);
            if (Response != null)
                return Response;
            Response = await next();
            await cacheService.SetAsync(request.Key, Response, request.Expiration, cancellationToken);
            return Response;
        }
    }
}
