using Modules.Orders.Domain.ValueObjects;

namespace Modules.Common.Application.Validators;

public static class LanguageValidator
{
    public static bool Must<T2>(IDictionary<Language, T2> objects)
    {
        return objects.Count >= 1 && objects.ContainsKey(Language.en);

    }

    public static string Message => "you need to provide the language definition for english";
}
