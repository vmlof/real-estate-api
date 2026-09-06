using Homely.Domain.Enums;

namespace Homely.Domain.Entities;

public class Tenant
{
    protected Tenant()
    {
    }

    public Tenant(int userId, string name, string phoneNumber)
    {
        UserId = userId;
        Name = name;
        PhoneNumber = phoneNumber;
    }

    public int Id { get; set; }
    public string Name { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public int UserId { get; private set; }
    public User User { get; private set; } = null!;

    private readonly List<RentalApplication> _applications = new();
    public IReadOnlyCollection<RentalApplication> Applications => _applications.AsReadOnly();

    private readonly List<Lease> _leases = new();
    public IReadOnlyCollection<Lease> Leases => _leases.AsReadOnly();

    private readonly List<Property> _favoritedProperties = new();
    public IReadOnlyCollection<Property> FavoritedProperties => _favoritedProperties.AsReadOnly();

    public void AddApplication(RentalApplication application)
    {
        _applications.Add(application);
    }

    public void AddLease(Lease lease)
    {
        _leases.Add(lease);
    }

    public void FavoriteProperty(Property property)
    {
        if (!_favoritedProperties.Contains(property))
            _favoritedProperties.Add(property);
    }
}