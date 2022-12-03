namespace TodoApp.Core.Repositories;
public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    void Update(T entity);
    void Add(T entity);
    void Delete(T entity);
}