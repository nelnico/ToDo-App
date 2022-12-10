using TodoApp.Core.Common.Pagination;
using TodoApp.Core.Dtos.Todos;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Repositories;

public interface ITodoItemRepository : IRepository<TodoItem>
{
    Task<PagedList<TodoItemDto>> SearchForUserAsync(AppUser user, TodoSearchParams parameters);
}