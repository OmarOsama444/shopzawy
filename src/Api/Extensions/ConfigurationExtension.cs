namespace Shopizawy.Api.Extensions;

internal static class ConfigurationExtension
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, params string[] modules)
    {
        foreach (var module in modules)
        {
            configurationBuilder.AddJsonFile($"modules.{module}.json", false, true);
            configurationBuilder.AddJsonFile($"modules.{module}.development.json", true, true);
        }
    }
}
