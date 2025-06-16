namespace shopzawy.Extensions;

internal static class ConfigurationExtension
{
    internal static void AddModuleConfiguration(this IConfigurationBuilder configurationBuilder, params string[] modules)
    {
        configurationBuilder
                .AddJsonFile($"appsettings.json", false, true)
                .AddJsonFile($"appsettings.Development.json", true, true)
                .AddEnvironmentVariables();
        foreach (var module in modules)
        {
            configurationBuilder
                .AddJsonFile($"modules.{module}.json", false, true)
                .AddJsonFile($"modules.{module}.Development.json", true, true)
                .AddEnvironmentVariables();
        }
    }
}
