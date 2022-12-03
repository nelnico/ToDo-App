namespace TodoApp.Core.Entities;

public class TodoItem : BaseEntity
{
    public DateTime? DueDate { get; set; }
    public int PriorityId { get; set; }
    public int StatusId { get; set; }
    public AppUser AppUser { get; set; }
    public Category Category{ get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Picture Picture { get; set; }
    public IList<Note> Notes { get; set; }
}