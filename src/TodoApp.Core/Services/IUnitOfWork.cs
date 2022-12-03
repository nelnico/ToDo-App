using TodoApp.Core.Repositories;

namespace TodoApp.Core.Services;
public interface IUnitOfWork
{
    ITodoItemRepository TodoItems { get; }
    Task<bool> CompleteAsync();
    bool Complete();
}