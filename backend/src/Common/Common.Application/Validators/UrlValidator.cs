namespace Common.Application.Validators;

public static class UrlValidator
{
    public static bool Must(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
    }
    public static string Message => "Invalid URL. Must be a valid HTTP or HTTPS link.";
}

