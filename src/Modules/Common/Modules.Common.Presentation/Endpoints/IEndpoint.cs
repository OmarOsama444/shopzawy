using Microsoft.AspNetCore.Routing;
using Modules.Orders.Domain.ValueObjects;

namespace Modules.Common.Presentation.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
    public record LocalizedText(IDictionary<Language, string> translations);

}
