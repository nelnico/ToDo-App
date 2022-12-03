namespace TodoApp.Core.Entities;

public class Category : BaseEntity
{
    public AppUser AppUser { get; set; }
    public string Name { get; set; }
}