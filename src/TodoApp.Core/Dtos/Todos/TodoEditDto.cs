using TodoApp.Core.Enums;

namespace TodoApp.Core.Dtos.Todos;

public class TodoEditDto
{
    public int Id { get; set; }
    public int PriorityId { get; set; } = PriorityEnum.Medium.Id;
    public int StatusId { get; set; } = StatusEnum.NotStarted.Id;
    public string Title { get; set; }
    public string Description { get; set; }
}