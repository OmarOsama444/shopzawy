using Microsoft.AspNetCore.Routing;
using Common.Domain.ValueObjects;
namespace Common.Presentation.Endpoints
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
    public class LocalizedText()
    {
        private IDictionary<Language, string> Translations = new Dictionary<Language, string>();
        public IDictionary<Language, string> translations
        {
            get
            {
                return Translations?
             .Where(kv => kv.Value != null)
             .ToDictionary(kv => kv.Key, kv => kv.Value)
             ?? [];
            }
            set
            {
                Translations = value;
            }
        }
    }

}
