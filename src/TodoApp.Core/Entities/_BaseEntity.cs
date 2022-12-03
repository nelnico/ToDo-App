namespace TodoApp.Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? Updated { get; set; }
}