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
    private readonly List<Lease> _leases = new();
    private readonly List<FavoriteProperty> _favoriteProperties = new();
    public IReadOnlyCollection<RentalApplication> Applications => _applications.AsReadOnly();
    public IReadOnlyCollection<Lease> Leases => _leases.AsReadOnly();
    public IReadOnlyCollection<FavoriteProperty> FavoriteProperties => _favoriteProperties.AsReadOnly();
}