namespace Modules.Orders.Domain.ValueObjects;

public static class SpecDataType
{
    public static string String => "string";
    public static string Booleon => "booleon";
    public static string Color => "color";
    public static string Number => "number";
    public static bool ValidKey(string key)
    {
        return
            key == SpecDataType.String ||
            key == SpecDataType.Booleon ||
            key == SpecDataType.Color ||
            key == SpecDataType.Number;
    }
}
