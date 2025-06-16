using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Modules.Common.Domain;
using Modules.Common.Domain.Exceptions;


namespace Modules.Common.Application.Extensions
{
    public static class ResultExtension
    {
        public static IResult ExceptionToResult(this Result result)
        {
            Exception ex = result.exception!;
            return ex switch
            {
                NotFoundException nf => Results.Problem(
                    title: nf.Code,
                    detail: nf.Message,
                    statusCode: StatusCodes.Status404NotFound
                ),
                BadRequestException br => Results.Problem(
                    title: "Bad Request",
                    detail: br.Message,
                    statusCode: StatusCodes.Status400BadRequest,
                    extensions: new Dictionary<string, object?> {
                        { "Validation Errors" , br.Errors }
                    }
                ),
                ConflictException ce => Results.Problem(
                    title: ce.Code,
                    detail: ce.Message,
                    statusCode: StatusCodes.Status409Conflict
                ),
                NotAuthorizedException na => Results.Problem(
                    title: na.Code,
                    detail: na.Message,
                    statusCode: StatusCodes.Status401Unauthorized
                ),
                ExpiredException ee => Results.Problem(
                    title: ee.Code,
                    detail: ee.Message,
                    statusCode: StatusCodes.Status403Forbidden
                ),
                _ => Results.Problem(
                    title: "Internal Server Error",
                    detail: "An unexpected error occurred.",
                    statusCode: StatusCodes.Status500InternalServerError
                )
            };
        }
    }
}
