namespace TodoApp.Core.Common.Pagination;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string OrderBy { get; set; } = string.Empty;
    public string OrderDirection { get; set; } = "asc";
}