using Microsoft.AspNetCore.Routing;
using Common.Domain.ValueObjects;
namespace Common.Presentation.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
    public record LocalizedText(IDictionary<Language, string> translations);

}
