using System.Text.Json;

namespace TodoApp.Api.Common.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage,
            int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

public class PaginationHeader
{
    public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
    {
        CurrentPage = currentPage;
        ItemsPerPage = itemsPerPage;
        TotalItems = totalItems;
        TotalPages = totalPages;
    }

    public int CurrentPage { get; set; }
    public int ItemsPerPage { get; set; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
}