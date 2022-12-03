using TodoApp.Core.Common.Pagination;
using TodoApp.Core.Dtos;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Repositories;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    Task<PagedList<TodoItemDto>> SearchAsync(TodoSearchParams parameters);
}