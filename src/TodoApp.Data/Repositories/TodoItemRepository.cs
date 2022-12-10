using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Common.Pagination;
using TodoApp.Core.Dtos.Todos;
using TodoApp.Core.Entities;
using TodoApp.Core.Repositories;

namespace TodoApp.Data.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public TodoItemRepository(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }


    public async  Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _dataContext.TodoItems.ToListAsync();
    }

    public async Task<TodoItem> GetByIdAsync(int id)
    {
        return await _dataContext.TodoItems.FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(TodoItem entity)
    {
        entity.Updated = DateTime.Now;
        _dataContext.Entry(entity).State = EntityState.Modified;
    }

    public void Add(TodoItem entity)
    {
        _dataContext.TodoItems.Add(entity);
    }

    public void Delete(TodoItem entity)
    {
        _dataContext.TodoItems.Remove(entity);
    }

    public async Task<PagedList<TodoItemDto>> SearchForUserAsync(AppUser user, TodoSearchParams parameters)
    {
        var query = _dataContext.TodoItems.AsQueryable().Where(x => x.AppUser == user);

        if (!string.IsNullOrEmpty(parameters.Query))
            query = query.Where(x => x.Title.Contains(parameters.Query, StringComparison.InvariantCultureIgnoreCase) ||
                                     x.Description.Contains(parameters.Query,
                                         StringComparison.InvariantCultureIgnoreCase));

        if (parameters.StatusIds != null && parameters.StatusIds.Any())
            query = query.Where(x => parameters.StatusIds.Contains(x.StatusId));

        return await PagedList<TodoItemDto>.CreateAsync(query
                .ProjectTo<TodoItemDto>(_mapper
                    .ConfigurationProvider).AsNoTracking(),
            parameters.PageNumber, parameters.PageSize);
    }
    
}