namespace TodoApp.Core.Common.Pagination;

public class TodoSearchParams : PaginationParams
{
    public string AppUserId { get; set; }
    public string Query { get; set; }
    public int? CategoryId { get; set; }
    public int[] StatusIds { get; set; }
}