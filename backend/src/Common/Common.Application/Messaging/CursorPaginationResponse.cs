namespace Common.Application.Messaging;

public class CursorMetaData
{
    public string? NextCursor { get; init; }
    public string? PreviousCursor { get; init; }
    public int PageSize { get; init; }
    public int ReturnedCount { get; init; }
}
public class CursorPaginationResponse<T>
{
    public ICollection<T> Data { get; init; }
    public CursorMetaData MetaData { get; init; }

    public CursorPaginationResponse(ICollection<T> data, string? nextCursor, string? prevCursor, int pageSize)
    {
        Data = data;
        MetaData = new CursorMetaData
        {
            NextCursor = nextCursor,
            PreviousCursor = prevCursor,
            PageSize = pageSize,
            ReturnedCount = data.Count
        };
    }
}
