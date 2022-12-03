using System.Reflection;

namespace TodoApp.Core.Enums;

public abstract class Enumeration : IComparable
{
    protected Enumeration(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public int Id { get; set; }


    public int CompareTo(object other)
    {
        return Id.CompareTo(((Enumeration)other).Id);
    }

    public override string ToString()
    {
        return Name;
    }

    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);

        return fields.Select(f => f.GetValue(null)).Cast<T>();
    }

    public static T GetById<T>(int id) where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);

        var allFields = fields.Select(f => f.GetValue(null)).Cast<T>();
        return allFields.FirstOrDefault(x => x.Id == id);
    }

    public static T GetByName<T>(string name) where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);

        var allFields = fields.Select(f => f.GetValue(null)).Cast<T>();
        return allFields.FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public static T GetByDescription<T>(string description) where T : Enumeration
    {
        var fields = typeof(T).GetFields(BindingFlags.Public |
                                         BindingFlags.Static |
                                         BindingFlags.DeclaredOnly);

        var allFields = fields.Select(f => f.GetValue(null)).Cast<T>();
        return allFields.FirstOrDefault(x =>
            x.Description.Equals(description, StringComparison.InvariantCultureIgnoreCase));
    }

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration;

        if (otherValue == null)
            return false;

        var typeMatches = GetType() == obj.GetType();
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    protected bool Equals(Enumeration other)
    {
        return string.Equals(Name, other.Name) && Id == other.Id;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Id;
        }
    }
}