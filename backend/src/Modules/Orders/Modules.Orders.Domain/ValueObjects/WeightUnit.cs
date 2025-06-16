using System.Text.Json.Serialization;

namespace Modules.Orders.Domain.ValueObjects;

public enum WeightUnit
{
    Gram = 1,
    Kilogram = 2,
    Pound = 3,
    Ounce = 4,
    Milligram = 5,
    Ton = 6
}
