namespace Modules.Orders.Application.Dtos;

public class TranslatedSpecResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string DataType { get; set; } = string.Empty;
    public ICollection<string> Options { get; set; } = [];
}
