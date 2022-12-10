namespace TodoApp.Core.Dtos.Todos;

public class TodoItemDto
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
}