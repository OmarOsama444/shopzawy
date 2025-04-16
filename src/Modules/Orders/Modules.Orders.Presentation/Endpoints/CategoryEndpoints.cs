using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using Modules.Common.Application.Extensions;
using Modules.Common.Presentation.Endpoints;
using Modules.Orders.Application.UseCases;
using Modules.Orders.Application.UseCases.CreateCategory;
using Modules.Orders.Application.UseCases.CreateCategorySpecOption;
using Modules.Orders.Application.UseCases.GetCategory;
using Modules.Orders.Application.UseCases.GetMainCategories;
using Modules.Orders.Application.UseCases.PaginateCategories;
using Modules.Orders.Application.UseCases.UpdateCategory;

namespace Modules.Orders.Presentation.Endpoints;

public class CategoryEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/categories").WithTags("Categories");

        group.MapPost("", async ([FromBody] CreateCategoryCommand request, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(request);
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("main", async (ISender sender) =>
        {
            var result = await sender.Send(new GetMainCategoriesQuery());
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("", async ([FromServices] ISender sender, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string? nameFilter = null) =>
        {
            var result = await sender.Send(new PaginateCategoryQuery(pageNumber, pageSize, nameFilter));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("{name}", async ([FromRoute] string name, [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new GetCategoryByNameQuery(name));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapPut("{name}", async ([FromRoute] string name, [FromBody] UpdateCategoryRequestDto request, ISender sender) =>
        {
            var result = await sender.Send(new UpdateCategoryCommand(name, request.Description, request.Order, request.ImageUrl));
            return result.isSuccess ? Results.NoContent() : result.ExceptionToResult();
        });

        /// <summary>
        ///  allowed data types : string booleon color number
        /// </summary>

        group.MapPost("{name}/specs", async (
            [FromRoute] string name,
            [FromBody] CreateCategorySpecRequestDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCategorySpecCommand(name, request.name, request.dataType));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        /// <summary>
        ///  you can't add color options as it is handled by the system
        /// </summary>

        group.MapPost("{name}/specs/{id}", async (
            [FromRoute] string name,
            [FromRoute] Guid id,
            [FromBody] CreateCategorySpecOptionRequestDto request,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(new CreateCategorySpecOptionCommand(name, id, request.value));
            return result.isSuccess ? Results.Ok(result.Value) : result.ExceptionToResult();
        });

        group.MapGet("specs/{id}", async (
            [FromRoute] Guid id,
            [FromServices] ISender sender) =>
        {
            var result = await sender.Send(null);
        });

    }
    public record CreateCategorySpecOptionRequestDto(string value);
    public record CreateCategorySpecRequestDto(string name, string dataType);
    public record UpdateCategoryRequestDto(string? Description, int? Order, string? ImageUrl);

}
