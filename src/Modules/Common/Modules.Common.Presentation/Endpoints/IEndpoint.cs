using Microsoft.AspNetCore.Routing;

namespace Modules.Common.Presentation.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
