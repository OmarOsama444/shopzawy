namespace Modules.Catalog.Application.Abstractions.Dto;

public class PaginationResponse<T>
{
    public ICollection<T> Data { get; set; } = [];
    public MetaData Meta { get; set; } = new();
}

public class MetaData
{
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public int TotalItems { get; set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;
}