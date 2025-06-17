using Common.Domain;
using Common.Domain.Exceptions;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Common.Application.Behaviour
{
    public class ValidationPiplineBehaviour<TRequest, TResponse>(
        IValidator<TRequest> validator
        ) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class
        where TResponse : Result
    {
        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
            )
        {
            var validationResults = await validator.ValidateAsync(request, cancellationToken);
            if (validationResults.IsValid)
                return await next();

            if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
            {
                Type resultType = typeof(TResponse).GetGenericArguments()[0];

                MethodInfo? failureMethod = typeof(Result<>)
                    .MakeGenericType(resultType)
                    .GetMethod(nameof(Result<Object>.Failure));

                MethodInfo? failureMethodGeneric = failureMethod?.MakeGenericMethod(resultType);

                if (failureMethodGeneric != null)
                {
                    return (TResponse)failureMethodGeneric.Invoke(null, new object[] { new BadRequestException(validationResults.ToDictionary()) })!;
                }
            }

            return (TResponse)Result.Failure(new BadRequestException(validationResults.ToDictionary()));

        }
    }
}
