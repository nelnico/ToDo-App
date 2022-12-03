using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Common.Pagination;
using TodoApp.Core.Dtos;
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


    public Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TodoItem> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(TodoItem entity)
    {
        throw new NotImplementedException();
    }

    public void Add(TodoItem entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(TodoItem entity)
    {
        throw new NotImplementedException();
    }

    public async Task<PagedList<TodoItemDto>> SearchAsync(TodoSearchParams parameters)
    {
        var query = _dataContext.TodoItems.AsQueryable();

        if (!string.IsNullOrEmpty(parameters.AppUserId))
            query = query.Where(x => x.AppUser.Id == parameters.AppUserId);

        if (!string.IsNullOrEmpty(parameters.Query))
            query = query.Where(x => x.Title.Contains(parameters.Query, StringComparison.InvariantCultureIgnoreCase) ||
                                     x.Description.Contains(parameters.Query,
                                         StringComparison.InvariantCultureIgnoreCase));

        if (parameters.CategoryId != null)
            query = query.Where(x => x.Category.Id == parameters.CategoryId);


        if (parameters.StatusIds != null && parameters.StatusIds.Any())
            query = query.Where(x => parameters.StatusIds.Contains(x.StatusId));

        return await PagedList<TodoItemDto>.CreateAsync(query
                .ProjectTo<TodoItemDto>(_mapper
                    .ConfigurationProvider).AsNoTracking(),
            parameters.PageNumber, parameters.PageSize);
    }
}