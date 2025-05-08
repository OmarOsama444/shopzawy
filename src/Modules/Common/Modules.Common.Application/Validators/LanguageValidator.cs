namespace Modules.Common.Application.Validators;

public static class LanguageValidator
{
    public static bool Must<T1, T2>(IDictionary<T1, T2> objects)
    {
        return objects.Count == 2;
    }

    public static string Message => "Your Missing the defination for one of the required languages";
}
