namespace Homely.Domain.Entities;

public record Coordinate(double Latitude, double Longitude);

public class Location
{
    protected Location()
    {
    }

    public Location(string address, string city, string state, string country, string postalCode, double latitude,
        double longitude)
    {
        Address = address;
        City = city;
        State = state;
        Country = country;
        PostalCode = postalCode;
        SetCoordinates(latitude, longitude);
    }

    public int Id { get; private set; }
    public string Address { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string Country { get; private set; } = string.Empty;

    public string PostalCode { get; private set; } = string.Empty;
    public Coordinate Coordinates { get; private set; }

    private readonly List<Property> _properties = new();
    public IReadOnlyCollection<Property> Properties => _properties.AsReadOnly();

    public void SetCoordinates(double latitude, double longitude)
    {
        Coordinates = new Coordinate(latitude, longitude);
    }
}