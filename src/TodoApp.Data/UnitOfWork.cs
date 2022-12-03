using AutoMapper;
using TodoApp.Core.Repositories;
using TodoApp.Core.Services;
using TodoApp.Data.Repositories;

namespace TodoApp.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly DataContext _dataContext;

    public UnitOfWork(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        TodoItems = new TodoItemRepository(_dataContext, mapper);
    }

    public ITodoItemRepository TodoItems { get; }

    public async Task<bool> CompleteAsync()
    {
        return await _dataContext.SaveChangesAsync() > 0;
    }

    public bool Complete()
    {
        return _dataContext.SaveChanges() > 0;
    }
}