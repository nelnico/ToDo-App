namespace TodoApp.Core.Enums;

public class StatusEnum : Enumeration
{
    public static StatusEnum NotStarted = new(1, "Not Started");
    public static StatusEnum OnHold = new(2, "On Hold");
    public static StatusEnum InProgress = new(3, "In Progress");
    public static StatusEnum Complete = new(4, "Completed");
    public static StatusEnum Rejected = new(5, "Rejected");
    public static StatusEnum Deleted = new(6, "Deleted");


    public StatusEnum(int id, string name)
        : base(id, name, string.Empty)
    {
    }
}