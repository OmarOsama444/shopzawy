using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace shopzawy.Swagger;

public class EnumSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var names = Enum.GetNames(context.Type);
            schema.Type = "string";
            schema.Enum = names.Select(name => new OpenApiString(name)).Cast<IOpenApiAny>().ToList();
        }
    }

}
