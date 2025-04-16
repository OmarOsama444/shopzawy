using Modules.Common.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;


namespace Modules.Common.Application.Behaviour
{
    public class RequestLoggingPiplineBehaviour<TRequest, TResponse>(
        ILogger<RequestLoggingPiplineBehaviour<TRequest, TResponse>> logger
        ) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            string moduleName = typeof(TRequest).FullName!.Split('.')[2];
            string requestName = typeof(TRequest).Name;

            using (LogContext.PushProperty("Module", moduleName))
            {
                logger.LogInformation("Process request {requestName}", requestName);
                TResponse response = await next();
                if (response.isSuccess)
                {
                    logger.LogInformation("Completed request {requestName}", requestName);
                }
                else
                {
                    using (LogContext.PushProperty("Error", response.exception, true))
                    {
                        logger.LogInformation("Error request {requestName}", requestName);
                    }
                }
                return response;

            }
        }
    }
}
