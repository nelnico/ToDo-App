namespace TodoApp.Core.Common.Pagination;

public class TodoSearchParams : PaginationParams
{
    public string Query { get; set; }
    public int[] StatusIds { get; set; }
}