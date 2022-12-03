namespace TodoApp.Core.Enums;

public class UserRoleEnum : Enumeration
{
    public static UserRoleEnum AdminRole = new(1, "Admin");
    public static UserRoleEnum UserRole = new(2, "User");

    public UserRoleEnum(int id, string name)
        : base(id, name, string.Empty)
    {
    }
}