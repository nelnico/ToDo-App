namespace TodoApp.Core.Entities;

public class Note : BaseEntity
{
    public TodoItem TodoItem { get; set; }
    public string Text { get; set; }
}