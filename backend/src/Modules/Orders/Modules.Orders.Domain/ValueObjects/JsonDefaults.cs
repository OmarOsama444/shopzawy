using System.Text.Json;
using System.Text.Json.Serialization;

namespace Modules.Orders.Domain.ValueObjects;

public static class JsonDefaults
{
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        // No property naming policy change
        PropertyNamingPolicy = null,
        WriteIndented = false, // Compact JSON
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = false // Case-sensitive (keep as is)
    };
}