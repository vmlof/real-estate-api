namespace Homely.Domain.Entities;

public class Manager
{
    protected Manager()
    {
    }

    public Manager(int userId, string name, string phoneNumber)
    {
        UserId = userId;
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    private readonly List<Property> _properties = new();
    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    public void AddProperty(Property property)
    {
        _properties.Add(property);
    }
}