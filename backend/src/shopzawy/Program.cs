using Modules.Common.Application;
using Modules.Common.Presentation.Endpoints;
using Modules.Common.Infrastructure;
using Modules.Users.Infrastructure;
using Modules.Orders.Infrastructure;
using Microsoft.AspNetCore.Http.Json;
using System.Text.Json.Serialization;
using shopzawy.Extensions;
using shopzawy.Middleware;
using shopzawy.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddCors(option =>
{
    option.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen(options =>
{
    options.UseInlineDefinitionsForEnums();
    options.SchemaFilter<EnumSchemaFilter>();
});
// pass the application layer assemblues here
// to use mediatr and fluent validation 
builder.Services.AddApplication(
    Modules.Users.Application.AssemblyRefrence.Assembly,
    Modules.Orders.Application.AssemblyRefrence.Assembly
    );
// pass the presentation layer assemblies here
// pass the Configuration Methods for masstransit if available in case of consuming events 
// pass the reddis connection string to use it via the cache service when needed
builder.Services.AddInfrastructure([], builder.Configuration);
// adding module config for all the modules 
builder.Configuration.AddModuleConfiguration("users", "orders");
// adding modules infrastructure
builder.Services.AddUsersModule(builder.Configuration);
builder.Services.AddOrdersModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapEndpoints();
app.UseStaticFiles();
app.UseCors();

app.MapPost("/upload", async (IFormFile image, HttpRequest request) =>
{
    if (image == null || image.Length == 0)
        return Results.BadRequest("No image uploaded.");

    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
    var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

    if (!allowedExtensions.Contains(extension))
        return Results.BadRequest("Unsupported file type.");

    var fileName = $"{Guid.NewGuid()}{extension}";
    var uploadPath = Path.Combine("wwwroot", "uploads", fileName);

    using var stream = new FileStream(uploadPath, FileMode.Create);
    await image.CopyToAsync(stream);

    var baseUrl = $"{request.Scheme}://{request.Host}";
    var imageUrl = $"{baseUrl}/uploads/{fileName}";

    return Results.Ok(new { imageUrl });
}).DisableAntiforgery();

app.Run();

